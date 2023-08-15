using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Challenge
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string FromUserId {get; set;}

    public string ToUserId {get; set;}

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime Start {get;set;}

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime End {get;set;}

    public bool IsEvent {get;set;}
}