using HotelManager.DataModels;
using MongoDB.Driver;

namespace HotelManager.Services
{
    public class HotelServices
    {
        readonly HotelContext context;

        public HotelServices(HotelContext context)
        {
            this.context = context;
        }

        public List<HotelDataModel> GetAllHotels()
        {
            return context.HotelCollection.Find(x => true).ToList();
        }

        public HotelDataModel GetHotelById(int id)
        {
            return context.HotelCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void CreateHotel(HotelDataModel hotel)
        {
            context.HotelCollection.InsertOne(hotel);
        }

        public void UpdateHotel(HotelDataModel hotel)
        {
            context.HotelCollection.ReplaceOne(x => x.Id == hotel.Id, hotel);
        }

        public void RemoveHotel(int id)
        {
            context.HotelCollection.DeleteOne(x => x.Id == id);
        }

        public List<HotelRoomTypeCountData> GetAllAvailableRooms(int id, DateTime startDate, DateTime endDate)
        {
            var hotel = context.HotelCollection.Find(x => x.Id == id).FirstOrDefault();
            var result = new List<HotelRoomTypeCountData>();
            foreach (var room in hotel.Rooms)
            {
                int count = RoomTypeServices.GetAllAvailableRooms(room, startDate, endDate).Count;
                result.Add(new HotelRoomTypeCountData(room.Id, count));
            }
            return result;
        }

        public int ReserveRoom(int hotelId, int roomTypeId, DateTime startDate, DateTime endDate)
        {
            var hotel = context.HotelCollection.Find(x => x.Id == hotelId).FirstOrDefault();
            if (hotel == null)
            {
                return -1;
            }
            var roomType = hotel.Rooms.Find(x => x.Id == roomTypeId);
            if (roomType == null)
            {
                return -1;
            }

            int RoomId = RoomTypeServices.ReserveRoom(roomType, startDate, endDate);
            if (RoomId == -1)
            {
                return -1;
            }
            context.HotelCollection.ReplaceOne(x => x.Id == hotelId, hotel);
            return RoomId;
        }

        public void CancelReservation(int hotelId, int roomTypeId, int roomId, DateTime startDate, DateTime endDate)
        {
            var hotel = context.HotelCollection.Find(x => x.Id == hotelId).FirstOrDefault();
            if (hotel == null)
            {
                return;
            }
            var roomType = hotel.Rooms.Find(x => x.Id == roomTypeId);
            if (roomType == null)
            {
                return;
            }

            RoomTypeServices.CancelReservation(roomType, startDate, endDate, roomId);
            context.HotelCollection.ReplaceOne(x => x.Id == hotelId, hotel);
        }

        public class HotelRoomTypeCountData
        {
            public int RoomId { get; set; }
            public int Count { get; set; }

            public HotelRoomTypeCountData() { }

            public HotelRoomTypeCountData(int roomId, int count)
            {
                RoomId = roomId;
                Count = count;
            }
        }
    }
}
