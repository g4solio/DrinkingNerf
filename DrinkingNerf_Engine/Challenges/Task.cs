using System.Diagnostics.Contracts;
using DrinkingNerf_Engine.Users;

public class Task : Challenge
{
    public Task(ChallengeContract contract) : base(contract)
    {
    }

    public override bool IsVisible(UserId userId)
    {
        return this._contract.From.TargetId.Equals(userId) || _contract.To.TargetId.Equals(userId);
    }
}