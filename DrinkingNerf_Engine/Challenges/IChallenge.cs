using DrinkingNerf_Engine.Users;

public interface IChallenge
{
    bool IsApplicableTo(UserId userId);

    bool IsApplicableFrom(UserId userId);

    bool IsApplicableByTime(DateTimeUniversal time);
    void Apply(Bang bang, User from, User to);

    bool IsVisible(UserId userId);
}