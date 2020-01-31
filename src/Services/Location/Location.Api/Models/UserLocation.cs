using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MyBlog.Location.Api.Models
{
    public class UserLocation
    {
        [BsonIgnoreIfDefault, BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }
        public int LocationId { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}
