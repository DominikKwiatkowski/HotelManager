using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManager.DataModels
{
    public class RoomTypeDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public List<DateDataModel> Calendar { get; set; }
    }
}
