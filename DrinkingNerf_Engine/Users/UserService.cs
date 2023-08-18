using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;
using DrinkingNerf_Engine.Exceptions;

namespace DrinkingNerf_Engine.Users
{
    public class UserService
    {
        private readonly IUserRepository<User> _userCtx;
        public UserService(IUserRepository<User> userDbContext)
        {
            _userCtx = userDbContext;
        }

        public UserId GetUserIdByName(string name)
        {
            var id = _userCtx.GetUserIdByName(name);

            if (string.IsNullOrEmpty(id))
                throw new UsernameNotFoundExceptions(name);

            return new UserId()
            {
                Id = id
            };
        }

        public User GetUser(UserId from)
        {
            return _userCtx.GetUser(from.Id);
        }

        public void UpdateUser(User fromUser)
        {
            _userCtx.UpdateUser(fromUser);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userCtx.GetUsers();
        }

        public void AddUser(string userName)
        {
            _userCtx.AddUser(new User()
            {
                UserId = new UserId(),
                Name = userName,
                Score = 0,
                Ammunitions = RULE_SET.DefaultAmmo
            });
        }

        public void DeleteUser(User user)
        {
            _userCtx.RemoveUser(user);
        }
    }
}