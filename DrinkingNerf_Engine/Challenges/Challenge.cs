using DrinkingNerf_Engine.Users;

public abstract class Challenge : IChallenge
{
    protected readonly ChallengeContract _contract;
    public readonly string Name;

    protected Challenge(ChallengeContract contract, string name)
    {
        _contract = contract;
        this.Name = name;
    }

    public void Apply(Bang bang, ref int HitBonus, ref int DamageMalus)
    {
        HitBonus = _contract.From.Modifier(HitBonus);
        DamageMalus = _contract.To.Modifier(DamageMalus);
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
