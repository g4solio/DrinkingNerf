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
                        TargetId = new() { Id = challenge.FromUserId }
                    },
                    To = new()
                    {
                        TargetId = new() { Id = challenge.ToUserId }
                    }
                };
                yield return challenge.IsEvent ? new Event(ctr) : new Task(ctr);
            }
        }
    }
}