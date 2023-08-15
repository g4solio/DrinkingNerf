using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class ChallengeService : IChallengesRepository
{
    private readonly IMongoCollection<Challenge> _challengeCollection;

    public ChallengeService(IOptions<DBSettings> dbSettings)
    {
       var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

       var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

       _challengeCollection = targetDb.GetCollection<Challenge>(dbSettings.Value.UserCollectionName);
    }

    public IEnumerable<IChallenge> GetChallenges()
    {
        foreach(var challenge in _challengeCollection.AsQueryable())
        {
            ChallengeContract ctr = new ()
            {
                StartDateTime = challenge.Start,
                EndDateTime = challenge.End,
                //TODO FINISH MAP
            };
            yield return challenge.IsEvent ? new Event(ctr) : new Task(ctr); 
        }
    }
}