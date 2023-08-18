using System.Diagnostics.Contracts;
using DrinkingNerf_Engine.Users;

namespace DrinkingNerf_Engine.Challenges
{
    public class Task : Challenge
    {
        public Task(ChallengeContract contract, string name) : base(contract, name)
        {
        }

        public override bool IsVisible(UserId userId)
        {
            return _contract.From.TargetId.Equals(userId) && DateTime.Today <= _contract.EndDateTime.Date && DateTime.Today >= _contract.StartDateTime.Date;
        }
    }
}