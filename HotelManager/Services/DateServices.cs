using HotelManager.DataModels;

namespace HotelManager.Services
{
    public class DateServices
    {
        public static IEnumerable<int> GetAllAvailableRooms(DateDataModel date)
        {
            return date.Rooms.Where(r => r.IsAvailable).Select(r => r.Id).ToList();
        }

        public static bool ReserveRoom(DateDataModel date, int roomId)
        {
            var room = date.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null || !room.IsAvailable)
            {
                return false;
            }

            room.IsAvailable = false;
            return true;
        }

        public static bool CancelReservation(DateDataModel date, int roomId)
        {
            var room = date.Rooms.FirstOrDefault(r => r.Id == roomId);
            if (room == null || room.IsAvailable)
            {
                return false;
            }

            room.IsAvailable = true;
            return true;
        }
    }
}
