using System.Diagnostics.Contracts;

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