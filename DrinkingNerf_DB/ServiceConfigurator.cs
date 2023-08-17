using DrinkingNerf_DB.Services;
using DrinkingNerf_Engine.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

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
                    options.ChallengeCollectionName = dbConfiguration.GetSection("challengeCollectionName").Value;
                });
            //service.AddSingleton<Services.UserService>();
            service.AddSingleton<IUserRepository<DrinkingNerf_Engine.Users.User>, Services.UserService>();
            service.AddSingleton<IChallengesRepository, ChallengeService>();

        }
    }
}