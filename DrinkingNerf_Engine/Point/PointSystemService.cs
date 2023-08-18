using DrinkingNerf_Engine.Bangs;
using DrinkingNerf_Engine.Challenges;
using DrinkingNerf_Engine.Users;

public class PointSystemService
{

    private readonly ChallengeService _challengeServ;
    private readonly UserService _userServ;
    private readonly BangService _bangService;

    public PointSystemService(UserService userServ, ChallengeService challengeServ, BangService bangService)
    {
        _userServ = userServ;
        _challengeServ = challengeServ;
        _bangService = bangService;
    }

    public void RegisterBang(Bang bang)
    {

        var fromUser = _userServ.GetUser(bang.From);
        var toUser = _userServ.GetUser(bang.To);

        if (fromUser.Ammunitions < 1) return;

        var applicableChallenges = _challengeServ.GetChallengesApplicableFromBang(bang);

        int hitReward = bang.Outcome == Bang.OutcomeEnum.Hit ? RULE_SET.HitReward : 0;
        int damageMalus = bang.Outcome == Bang.OutcomeEnum.Hit ? RULE_SET.DamageMalus : 0;

        foreach(var challenge in applicableChallenges)
            challenge.Apply(bang, ref hitReward, ref damageMalus);

        fromUser.Score += hitReward;
        toUser.Score -= damageMalus;

        fromUser.Ammunitions--;

        _bangService.RegisterBangOutcome(new BangOutcome()
        {
            Shooter = bang.From,
            Target = bang.To,
            Outcome = bang.Outcome,
            DateTime = bang.TimeOfBang,
            ShooterHitScoreModificator = hitReward
        });

        if (_bangService.ShouldRegainAmmo(bang.From))
            fromUser.Ammunitions += RULE_SET.AmmoToRegain;

        _userServ.UpdateUser(fromUser);
        _userServ.UpdateUser(toUser);

    }

    public User[] GetLeaderboard()
    {
        return _userServ.GetUsers().OrderByDescending(u => u.Score).ToArray();
    }

}
