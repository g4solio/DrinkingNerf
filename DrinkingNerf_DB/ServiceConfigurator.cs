using DrinkingNerf_DB.Services;
using DrinkingNerf_Engine.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using DrinkingNerf_Engine.Bangs;

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
                    options.BangCollectionName = dbConfiguration.GetSection("bangCollectionName").Value;
                });
            //service.AddSingleton<Services.UserService>();
            service.AddSingleton<IUserRepository<DrinkingNerf_Engine.Users.User>, Services.UserService>();
            service.AddSingleton<IChallengesRepository, ChallengeService>();
            service.AddSingleton<IBangRepository, Services.BangService>();
            service.AddSingleton<ChallengeModelService>();

            BsonSerializer.RegisterSerializer(typeof(DateTime), DateTimeSerializer.LocalInstance);

        }
    }
}