using DrinkingNerf_Engine.Users;

namespace DrinkingNerf_Engine.Challenges
{
    public class Event : Challenge
    {
        public Event(ChallengeContract contract, string name) : base(contract, name)
        {
        }

        public override bool IsVisible(UserId userId)
        {
            return DateTime.Today <= _contract.EndDateTime.Date && DateTime.Today >= _contract.StartDateTime.Date;
        }
    }
}