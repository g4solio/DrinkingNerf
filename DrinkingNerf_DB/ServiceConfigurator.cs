using DrinkingNerf_DB.Services;
using DrinkingNerf_Engine.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrinkingNerf_DB
{
    public static class ServiceConfigurator
    {
        public static void Configure(IServiceCollection service, IConfigurationSection dbConfiguration)
        {
            service.Configure<DBSettings>(
                options=>
                {
                    options.ConnectionString = dbConfiguration.GetSection("dbConfiguration").Value;
                    options.DBName = dbConfiguration.GetSection("dbName").Value;
                    options.UserCollectionName = dbConfiguration.GetSection("userCollectionName").Value;
                });
            service.AddSingleton<Services.UserService>();
            service.AddTransient<IUserRepository<DrinkingNerf_Engine.Users.User>, Services.UserService>();
            service.AddTransient<IChallengesRepository, ChallengeService>();

        }
    }
}