public class ChallengeService
{
    public readonly IChallengesRepository _challengesCtx;
    public ChallengeService(IChallengesRepository challengesRepository)
    {
        _challengesCtx = challengesRepository;
    }

    public IChallenge[] GetChallengesApplicableFromBang(Bang shotInfo)
    {
        bool IsApplicable(IChallenge challenge) => 
            challenge.IsApplicableFrom(shotInfo.From) 
            && challenge.IsApplicableTo(shotInfo.To) 
            && challenge.IsApplicableByTime((DateTimeUniversal)shotInfo.TimeOfBang);

        IEnumerable<IChallenge> FilterChallenges(IEnumerable<IChallenge> challenges)
        {
            foreach(var challenge in challenges)
                if(IsApplicable(challenge))
                    yield return challenge;
        }

        var challenges = _challengesCtx.GetChallenges();

        return FilterChallenges(challenges).ToArray();
    }

    public IChallenge[] GetChallengesVisibleByUser(UserId userId)
    {
        IEnumerable<IChallenge> FilterChallenges() => 
            _challengesCtx.GetChallenges().Where(c => c.IsVisible(userId));

        return FilterChallenges().ToArray();
    }

}
