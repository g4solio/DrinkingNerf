using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinkingNerf_Engine.Users;

namespace DrinkingNerf_DB.Models
{
    public class BangModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string ShooterId { get; set; }

        public string TargetId { get; set; }

        public bool IsHit { get; set; }

        public int ShooterHitScoreModificator { get; set; }

        public DateTime DateTime { get; set; }
    }
}
