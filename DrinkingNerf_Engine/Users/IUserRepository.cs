namespace DrinkingNerf_Engine.Users
{
    public interface IUserRepository<TUser>
    {
        void AddUser(TUser user);
        TUser GetUser(string id);
        string GetUserIdByName(string name);
        IEnumerable<TUser> GetUsers();
        void RemoveUser(TUser user);
        void UpdateUser(TUser fromUser);
    }
}