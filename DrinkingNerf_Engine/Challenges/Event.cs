using DrinkingNerf_Engine.Users;

public class Event : Challenge
{
    public Event(ChallengeContract contract, string name) : base(contract, name)
    {
    }

    public override bool IsVisible(UserId userId)
    {
        return true;
    }
}