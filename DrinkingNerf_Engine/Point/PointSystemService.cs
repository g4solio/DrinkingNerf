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

        foreach(var challenge in applicableChallenges)
            challenge.Apply(bang, from: fromUser, to: toUser);

        _userServ.UpdateUser(fromUser);
        _userServ.UpdateUser(toUser);
    }

}
