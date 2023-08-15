using DrinkingNerf_Engine;

public class Event : Challenge
{
    public Event(ChallengeContract contract) : base(contract)
    {
    }

    public override bool IsVisible(UserId userId)
    {
        return true;
    }
}