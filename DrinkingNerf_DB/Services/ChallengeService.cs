using AngouriMath;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DrinkingNerf_DB.Services
{
    public class ChallengeService : IChallengesRepository
    {
        private readonly IMongoCollection<ChallengeModel> _challengeCollection;

        public ChallengeService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

            _challengeCollection = targetDb.GetCollection<ChallengeModel>(dbSettings.Value.ChallengeCollectionName);
        }

        public IEnumerable<IChallenge> GetChallenges()
        {
            foreach (var challenge in _challengeCollection.AsQueryable())
            {
                ChallengeContract ctr = new()
                {
                    StartDateTime = challenge.Start,
                    EndDateTime = challenge.End,
                    TargetOutcome = Bang.OutcomeEnum.Hit,
                    From = new()
                    {
                        TargetId = new() { Id = challenge.FromUserId },
                        Modifier = BuildModifier(challenge.Formula)
                    },
                    To = new()
                    {
                        TargetId = new() { Id = challenge.ToUserId },
                        Modifier = (value) => value
                    },
                    
                };
                yield return challenge.IsEvent ? new Event(ctr, challenge.Name) : new Task(ctr, challenge.Name);
            }
        }

        private UserChallengeContext.BonusModifier BuildModifier(string formula)
        {
            if (string.IsNullOrEmpty(formula))
                return (value) => value;

            return (value) =>
            {
                Entity expr = formula.Replace("x", value.ToString());
                return (int)expr.EvalNumerical();
            };
        }


    }
}