using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using task.io.authentication.api.Services.Users;
using task.io.authentication.Application.Services.Common;
using task.io.authentication.Domain.Entities.Users;
using task.io.authentication.InfraStructure.Data;

namespace task.io.authentication.api
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IUserRepository, UserRepository>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services
            , IConfiguration configuration)
        {
            var mySqlConnection = configuration.GetConnectionString("Mysql");
            return services.AddDbContext<EFContext>(options =>
                     options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));
        }

        public static IServiceCollection AddBusinessServices(this IServiceCollection services
           )
        {
            return services
                .AddScoped<ITokenService, TokenService>()
                .AddScoped<IUserService,UserService>();
        }
    }
}