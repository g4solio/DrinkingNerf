namespace DrinkingNerf_Engine.Users
{
    public interface IUserRepository<TUser>
    {
        TUser GetUser(string id);
        string GetUserIdByName(string name);
        void UpdateUser(TUser fromUser);
    }
}