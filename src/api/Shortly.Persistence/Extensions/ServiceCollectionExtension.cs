using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shortly.Contract.Dependencies.DataAccess;
using Shortly.Contract.Dependencies.Services;
using Shortly.Contract.DependencyInjections.Repositories;
using Shortly.Domain.Entities.Identity;
using Shortly.Infrastructure.Abstractions.DataAccess;
using Shortly.Persistence.Contexts;
using Shortly.Persistence.Dependencies.Repositories;
using Shortly.Persistence.Dependencies.Services;
using Shortly.Persistence.Options;

namespace Shortly.Persistence.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddSqlServerPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind DatabaseOptions from appsettings.json
            services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseOptions"));

            // Bind IdentityOptions from appsettings.json
            services.Configure<IdentityOptions>(configuration.GetSection("IdentityOptions"));

            // Configure DbContext with SQL Server
            services.AddDbContextPool<ApplicationDbContext>((provider, optionsBuilder) =>
            {
                var databaseOptions = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

                optionsBuilder
                    .UseSqlServer(
                        databaseOptions.ConnectionString,
                        sqlOptions => sqlOptions.ExecutionStrategy(dependencies => new SqlServerRetryingExecutionStrategy(
                            dependencies: dependencies,
                            maxRetryCount: databaseOptions.MaxRetryCount,
                            maxRetryDelay: databaseOptions.MaxRetryDelay,
                            errorNumbersToAdd: databaseOptions.ErrorNumbersToAdd
                        ))
                        .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
                    .EnableDetailedErrors(databaseOptions.EnableDetailError)
                    .EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
            });

            // Configure Identity
            services.AddIdentityCore<ApplicationUser>(options => { })
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Load PasswordValidatorOptions from appsettings.json
            var serviceProvider = services.BuildServiceProvider();
            var passwordValidatorOptions = serviceProvider.GetService<IOptionsMonitor<PasswordValidatorOptions>>();

            services.Configure<PasswordValidatorOptions>(configuration.GetSection("PasswordOptions"));
        }

        public static void PersistenceConfigureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IUnitOfWorkDbContext<>), typeof(UnitOfWorkDbContext<>));
            services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.AddScoped(typeof(IRepositoryDbContext<,,>), typeof(RepositoryDbContext<,,>));

            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
