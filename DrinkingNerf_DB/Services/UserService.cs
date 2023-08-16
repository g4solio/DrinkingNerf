using DrinkingNerf_DB.Models;
using DrinkingNerf_Engine.Users;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DrinkingNerf_DB.Services
{
    public class UserService : IUserRepository<User>
    {
        private readonly IMongoCollection<UserModel> _userCollection;

        public UserService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

            _userCollection = targetDb.GetCollection<UserModel>(dbSettings.Value.UserCollectionName);
        }

        public User GetUser(string id)
        {
            var dataSource = _userCollection.Find(u => u.Id == id).Single();

            return new()
            {
                UserId = new()
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

        public IEnumerable<User> GetUsers()
        {
            return _userCollection.AsQueryable().Select(u => new User()
            {
                Name = u.Name,
                Score = u.Score,
                UserId = new()
                {
                    Id = u.Id
                }
            });
        }

        public void UpdateUser(User fromUser)
        {
            var update = Builders<UserModel>.Update.Set(u => u.Score, fromUser.Score);
            _userCollection.UpdateOne(u => u.Id == fromUser.UserId.Id, update);
        }
    }
}