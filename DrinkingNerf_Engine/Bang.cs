using DrinkingNerf_Engine.Users;

[Serializable]
public struct Bang
{
    public UserId From { get; init; }
    public UserId To {get; init;}
    public DateTime TimeOfBang {get; init;}
    public OutcomeEnum Outcome {get; init;}
    public enum OutcomeEnum
    {
        Missed,
        Hit
    };
}
