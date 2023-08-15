using System.Runtime.Serialization;
using DrinkingNerf_Engine.Exceptions;

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

        if(id <= 0)
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
    public UserId UserId {get; set;}
    public int Score {get; set;}

}

public interface IUserRepository<TUser>
{
    TUser GetUser(int id);
    int GetUserIdByName(string name);
    void UpdateUser(TUser fromUser);
}

public struct UserId
{
    public int Id { get; init; }
}