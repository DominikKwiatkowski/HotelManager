using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManager.DataModels
{
    public class RoomBookedModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
    }
}
