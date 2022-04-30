using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManager.DataModels
{
    public class HotelDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public List<RoomTypeDataModel> Rooms { get; set; }
        
    }
}
