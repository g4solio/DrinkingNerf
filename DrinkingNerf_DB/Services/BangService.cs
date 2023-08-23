using DrinkingNerf_DB.Models;
using DrinkingNerf_Engine.Bangs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkingNerf_DB.Services
{
    public class BangService : IBangRepository
    {
        private readonly IMongoCollection<BangModel> _bangCollection;

        public BangService(IOptions<DBSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);

            var targetDb = mongoClient.GetDatabase(dbSettings.Value.DBName);

            _bangCollection = targetDb.GetCollection<BangModel>(dbSettings.Value.BangCollectionName);
        }

        public void Add(BangOutcome bangOutcome)
        {
            _bangCollection.InsertOne(new BangModel()
            {
                ShooterId = bangOutcome.Shooter.Id,
                TargetId = bangOutcome.Target.Id,
                IsHit = bangOutcome.Outcome == Bang.OutcomeEnum.Hit ? true : false,
                DateTime = bangOutcome.DateTime,
                ShooterHitScoreModificator = bangOutcome.ShooterHitScoreModificator
            });
        }

        public IEnumerable<BangOutcome> GetBangs()
        {
            return _bangCollection.AsQueryable().Select(b => new BangOutcome()
            {
                Shooter = new DrinkingNerf_Engine.Users.UserId() { Id = b.ShooterId },
                Target = new DrinkingNerf_Engine.Users.UserId() { Id = b.TargetId },
                Outcome = b.IsHit ? Bang.OutcomeEnum.Hit : Bang.OutcomeEnum.Missed,
                DateTime = b.DateTime,
                ShooterHitScoreModificator = b.ShooterHitScoreModificator
            });
        }

        public void Delete(BangOutcome bang)
        {
            _bangCollection.DeleteOne(b => b.TargetId == bang.Target.Id && b.ShooterId == bang.Shooter.Id && b.DateTime == bang.DateTime);
        }
    }
}
