using DrinkingNerf_Engine.Challenges;
using DrinkingNerf_Engine.Users;

public class PointSystemService
{

    private readonly ChallengeService _challengeServ;
    private readonly UserService _userServ;

    public PointSystemService(UserService userServ, ChallengeService challengeServ)
    {
        _userServ = userServ;
        _challengeServ = challengeServ;
    }

    public void RegisterBang(Bang bang)
    {
        var applicableChallenges = _challengeServ.GetChallengesApplicableFromBang(bang);

        var fromUser = _userServ.GetUser(bang.From);
        var toUser = _userServ.GetUser(bang.To);

        int hitReward = RULE_SET.HitReward;
        int damageMalus = RULE_SET.DamageMalus;

        foreach(var challenge in applicableChallenges)
            challenge.Apply(bang, ref hitReward, ref damageMalus);

        fromUser.Score += hitReward;
        toUser.Score -= damageMalus;

        _userServ.UpdateUser(fromUser);
        _userServ.UpdateUser(toUser);
    }

    public User[] GetLeaderboard()
    {
        return _userServ.GetUsers().OrderBy(u => u.Score).ToArray();
    }

}
