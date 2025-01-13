using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shortly.API.Models;
using Shortly.Application.Abstractions;
using Shortly.Contract.Abstractions;
using Shortly.Contract.Dependencies.Services;
using Shortly.Contract.Services.V1.Identity;

namespace Shortly.API.Controllers.Identity
{

    [ApiVersion("1")]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoggerManager loggerManager;

        private readonly IUserServices _userServices;

        public AuthController(ILoggerManager loggerManager, IUserServices userServices)
        {
            this.loggerManager = loggerManager;
            this._userServices = userServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                Log.Logger.Error("Request is invalid");
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ApiReponse<object>
                {
                    Success = false,
                    Message = "Invalid input data",
                    Errors = errors
                });
            }

            var response = await _userServices.LoginAsync(request);

            return Ok(new ApiReponse<LoginResponse>
            {
                Success = true,
                Message = "Login successfully",
                Data = response
            });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody]RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                Log.Logger.Error("Request is invalid");
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
                return BadRequest(new ApiReponse<object>
                {
                    Success = false,
                    Message = "Invalid input data",
                    Errors = errors
                });
            }

            var (response, message) = await _userServices.RegisterAsync(request);

            if(response == null)
            {
                return BadRequest(new ApiReponse<object>
                {
                    Success = false,
                    Message = "Reigster failed",
                    Errors = new
                    {
                        Details = message
                    }
                });
            }

            return Ok(new ApiReponse<RegisterResponse>
            {
                Success = true,
                Message = "Register successfully",
                Data = response
            });
        }
    }
}
