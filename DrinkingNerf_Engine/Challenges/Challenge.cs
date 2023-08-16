using DrinkingNerf_Engine.Users;

public abstract class Challenge : IChallenge
{
    protected readonly ChallengeContract _contract;

    protected Challenge(ChallengeContract contract)
    {
        _contract = contract;
    }

    public void Apply(Bang bang, User from, User to)
    {
        from.Score += _contract.From.Modifier(RULE_SET.HitReward);
        to.Score -= _contract.To.Modifier(RULE_SET.DamageMalus);
    }

    public bool IsApplicableByTime(DateTimeUniversal time)
    {
        return time < _contract.EndDateTime.ToUniversalTime() && time > _contract.StartDateTime.ToUniversalTime();
    }

    public bool IsApplicableFrom(UserId userId)
    {
        return _contract.From.TargetId.Equals(userId);
    }

    public bool IsApplicableTo(UserId userId)
    {
        return _contract.To.TargetId.Equals(userId);
    }

    public abstract bool IsVisible(UserId userId);
}

public class DateTimeUniversal
{
    private readonly DateTime _this;

    public DateTimeUniversal(DateTime original)
    {
        _this = original.ToUniversalTime();
    }

    public static implicit operator DateTime(DateTimeUniversal universalTime) => universalTime._this;
    public static explicit operator DateTimeUniversal(DateTime original) => new(original.ToUniversalTime());
}

public class ChallengeContract
{
    public UserChallengeContext From;
    public UserChallengeContext To;

    public Bang.OutcomeEnum TargetOutcome;

    public DateTime StartDateTime;
    public DateTime EndDateTime;

}

public class UserChallengeContext
{
    public UserId TargetId;

    public BonusModifier Modifier;

    public delegate int BonusModifier(int bonus);
}
