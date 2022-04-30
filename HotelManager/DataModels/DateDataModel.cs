using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HotelManager.DataModels
{
    public class DateDataModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]        
        int Id { get; set; }
        public DateTime Date { get; set; }
        public List<RoomBookedModel> Rooms { get; set; }
    }
}
