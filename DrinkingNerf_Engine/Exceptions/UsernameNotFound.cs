namespace DrinkingNerf_Engine.Exceptions
{
    [Serializable]
    internal class UsernameNotFoundExceptions : Exception
    {
        public UsernameNotFoundExceptions()
        {
        }

        public UsernameNotFoundExceptions(string username) : base($"{username} not found")
        {
        }
    }
}