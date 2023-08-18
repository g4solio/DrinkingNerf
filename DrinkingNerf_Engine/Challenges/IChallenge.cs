using DrinkingNerf_Engine.Users;

public interface IChallenge
{
    bool IsApplicableTo(UserId userId);

    bool IsApplicableFrom(UserId userId);

    bool IsApplicableByTime(DateTime time);
    void Apply(Bang bang, ref int HitBonus, ref int DamageMalus);

    bool IsVisible(UserId userId);

    string Name();
    UserId Target();
    UserId Shooter();

}