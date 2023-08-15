using DrinkingNerf_Engine;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class UserService : IUserRepository<DrinkingNerf_Engine.User>
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(IOptions<DBSettings> dbSettings)
    {
       var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

       var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

       _userCollection = targetDb.GetCollection<User>(dbSettings.Value.UserCollectionName);
    }

    public DrinkingNerf_Engine.User GetUser(string id)
    {
        var dataSource = _userCollection.Find(u => u.Id == id).Single();

        return new()
        {
            UserId = new ()
                {
                    Id = dataSource.Id
                },
            Name = dataSource.Name,
            Score = dataSource.Score
        };
    }

    public string GetUserIdByName(string name)
    {
        return _userCollection.Find(u => u.Name == name).First().Id;
    }

    public void UpdateUser(DrinkingNerf_Engine.User fromUser)
    {
        var update = Builders<User>.Update.Set(u => u.Score, fromUser.Score);
        _userCollection.UpdateOne(u => u.Id == fromUser.UserId.Id, update);
    }
}