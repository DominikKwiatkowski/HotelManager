using HotelManager.DataModels;
using HotelManager.Services;
using MongoDB.Driver;

namespace HotelManager.Services
{
    public class RoomTypeServices
    {
        public static List<int> GetAllAvailableRooms(RoomTypeDataModel roomType, DateTime startDate, DateTime endDate)
        {
            List<int> availableList = new List<int>();
            // Get all days between startDate and endDate
            List<DateDataModel> days = roomType.Calendar.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
            List<List<int>> roomsAvailable = new List<List<int>>();

            foreach (DateDataModel day in days)
            {
                roomsAvailable.Add(DateServices.GetAllAvailableRooms(day).ToList());
            }

            availableList = roomsAvailable[0];
            // Find all available rooms. Rooms is available, if it exist in all days
            for (int i = 1; i < roomsAvailable.Count; i++)
            {
                availableList = availableList.Intersect(roomsAvailable[i]).ToList();
            }

            return availableList;
        }

        public static int ReserveRoom(RoomTypeDataModel roomType, DateTime startDate, DateTime endDate)
        {
            // Get all days between startDate and endDate
            List<DateDataModel> days = roomType.Calendar.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
            List<int> roomsAvailable = GetAllAvailableRooms(roomType, startDate, endDate);
                       
            if (roomsAvailable.Count == 0)
            {
                return -1;
            }
            // Create update definition, for each day, reserve room
            //Reserve room
            foreach (DateDataModel day in days)
            {
                DateServices.ReserveRoom(day, roomsAvailable[0]);
            }

            return roomsAvailable[0];
        }

        public static void CancelReservation(RoomTypeDataModel roomType, DateTime startDate, DateTime endDate, int roomNumber)
        {
            // Get all days between startDate and endDate
            List<DateDataModel> days = roomType.Calendar.Where(x => x.Date >= startDate && x.Date <= endDate).ToList();
            // Create update definition, for each day, cancel room
            //Cancel room
            foreach (DateDataModel day in days)
            {
                DateServices.CancelReservation(day, roomNumber);
            }
        }
    }
}
