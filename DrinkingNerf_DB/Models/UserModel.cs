using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DrinkingNerf_DB.Models
{
    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Score")]
        public int Score { get; set; }

    }
}