using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkingNerf_DB.Services
{
    public class ChallengeModelService
    {
        private readonly IMongoCollection<ChallengeModel> _challengeCollection;

        public ChallengeModelService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

            _challengeCollection = targetDb.GetCollection<ChallengeModel>(dbSettings.Value.ChallengeCollectionName);
        }

        public IEnumerable<ChallengeModel> GetChallenges()
        {
            return _challengeCollection.Find(c => true).ToEnumerable();
        }

        public void Delete(ChallengeModel challenge)
        {
            _challengeCollection.DeleteOne(c => c.Id == challenge.Id);
        }

        public void Update(ChallengeModel challenge)
        {
            _challengeCollection.ReplaceOne(c => c.Id == challenge.Id, challenge);
        }

        public void Add(ChallengeModel challenge)
        {
            _challengeCollection.InsertOne(challenge);
        }

    }
}
