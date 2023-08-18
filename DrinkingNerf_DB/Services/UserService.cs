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
            if (string.IsNullOrEmpty(id)) return null;

            var dataSource = _userCollection.Find(u => u.Id == id).SingleOrDefault();

            return MapUserDto(dataSource);
        }

        public string GetUserIdByName(string name)
        {
            return _userCollection.Find(u => u.Name == name).FirstOrDefault()?.Id;
        }

        public IEnumerable<User> GetUsers()
        {
            foreach(var uDTO in _userCollection.AsQueryable())
                yield return MapUserDto(uDTO);
        }

        private User MapUserDto(UserModel u)
        {
            if (u == null) return null;
            if(DateTime.Now > u.NextResetAmmo)
            {
                u.NextResetAmmo = DateTime.Today.AddDays(1).Date;
                u.Ammunitions = RULE_SET.DefaultAmmo; //TODO rewrite to bring RULE access to engine project
                _userCollection.ReplaceOne(x => x.Id == u.Id, u);
            }

            return new User()   
            {
                Name = u.Name,
                Score = u.Score,
                UserId = new()
                {
                    Id = u.Id
                },
                Ammunitions = u.Ammunitions
            };
        }

        public void UpdateUser(User fromUser)
        {
            var update = Builders<UserModel>.Update.Set(u => u.Score, fromUser.Score).Set(u => u.Ammunitions, fromUser.Ammunitions);
            _userCollection.UpdateOne(u => u.Id == fromUser.UserId.Id, update);
        }

        public void AddUser(User user)
        {
            _userCollection.InsertOne(new UserModel()
            {
                Name = user.Name,
                Score = user.Score,
                Ammunitions = user.Ammunitions,
                NextResetAmmo = DateTime.Today.AddDays(1)
            });
        }

        public void RemoveUser(User user)
        {
            _userCollection.DeleteOne(u => u.Id == user.UserId.Id);
        }
    }
}