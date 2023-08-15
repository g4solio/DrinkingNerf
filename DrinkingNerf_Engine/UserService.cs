using System.Runtime.Serialization;
using DrinkingNerf_Engine.Exceptions;

namespace DrinkingNerf_Engine
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

        internal User GetUser(UserId from)
        {
            return _userCtx.GetUser(from.Id);
        }

        internal void UpdateUser(User fromUser)
        {
            _userCtx.UpdateUser(fromUser);
        }
    }

    public class User
    {
        public UserId UserId { get; set; }

        public string Name {get; set;}
        public int Score { get; set; }

    }

    public interface IUserRepository<TUser>
    {
        TUser GetUser(string id);
        string GetUserIdByName(string name);
        void UpdateUser(TUser fromUser);
    }

    public struct UserId
    {
        public string Id { get; init; }
    }
}