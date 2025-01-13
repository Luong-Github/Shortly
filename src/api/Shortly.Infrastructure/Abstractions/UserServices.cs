
using Microsoft.AspNetCore.Identity;
using Serilog;
using Shortly.Application.Abstractions;
using Shortly.Contract.Abstractions;
using Shortly.Contract.Services.V1.Identity;
using Shortly.Domain.Entities.Identity;
using Shortly.Persistence.Constants;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shortly.Infrastructure.Abstractions
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IJwtTokenService _jwtToken;

        public UserServices(UserManager<ApplicationUser> userManager, IJwtTokenService jwtToken, SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _jwtToken = jwtToken;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // get user by email
            var user = await _userManager.FindByEmailAsync(request.Email);

            //if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            //{
            //    throw new UnauthorizedAccessException("Invalid username or password");
            //}

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, true, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return new LoginResponse
            {
                AccessToken = _jwtToken.GenerateAccessToken(claims),
                RefreshToken = _jwtToken.GenerateRefreshToken(),
                ExpiresAt = DateTime.UtcNow.AddMinutes(5)
            };
        }

        public Task<TokenRefreshResponse> RefreshTokenAsync(TokenRefreshRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<(RegisterResponse?, string?)> RegisterAsync(RegisterRequest request)
        {
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if(existingUser != null)
                {
                    Log.Logger.Error($"{request.Email} is already existed");
                    // defined new response
                    return (null, $"The email '{request.Email}' is already registered. Please use a different email.");
                }

                if ((await _roleManager.RoleExistsAsync(Roles.User)) == false)
                {
                    var roleResult = await _roleManager.CreateAsync(new ApplicationRole(Roles.User));

                    if(roleResult.Succeeded == false)
                    {
                        var errorRole = roleResult.Errors.Select(x => x.Description);
                        Log.Logger.Error($"Failed to create User role, detail {string.Join(",",errorRole)}");
                        return (null, $"Failed to create {request.Email}'");
                    }
                }

                ApplicationUser newUser = new ApplicationUser
                {
                    UserName = request.UserName,
                    Email = request.Email,
                    PasswordHash = request.Password,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var createNewUser = await _userManager.CreateAsync(newUser, request.Password);

                if(createNewUser.Succeeded  == false)
                {
                    var createNewUserErrors = createNewUser.Errors.Select(x => x.Description);
                    Log.Logger.Error($"Failed to create user, detail: {string.Join(",", createNewUserErrors)}");
                    return (null, $"Failed to create {request.Email}'");
                }

                // adding role to user
                var applyRoleToUser = await _userManager.AddToRolesAsync(newUser, [Roles.User]);
                
                if(applyRoleToUser.Succeeded == false)
                {
                    var applyRoleToUserErrors = applyRoleToUser.Errors.Select(x => x.Description);
                    Log.Logger.Error($"Failed to create user, detail: {string.Join(",", applyRoleToUserErrors)}");
                    return (null, $"Failed to create {request.Email}'");
                }

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString())
                };


                return (new RegisterResponse
                {
                    AccessToken = _jwtToken.GenerateAccessToken(claims),
                    RefreshToken = _jwtToken.GenerateRefreshToken(),
                    ExpiredTime = DateTime.UtcNow.AddMinutes(5)
                }, null);
            }
            catch(Exception ex)
            {
                throw new Exception("Something went wrong");
            }
        }


    }
}
