using HotelManager.DataModels;
using HotelManager.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace HotelManager.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HotelManagerController : ControllerBase
    {
        private readonly HotelServices _hotelService;

        private readonly ILogger<HotelManagerController> _logger;

        public HotelManagerController(ILogger<HotelManagerController> logger, HotelContext hotelContext)
        {
            _logger = logger;
            _hotelService = new HotelServices(hotelContext);
        }

        [HttpGet(Name = "GetHotels")]
        public IEnumerable<HotelDataModel> GetHotels()
        {
            // logging scheme [timestamp] [url] [message]
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetHotels]GetHotels called");
            var hotels = _hotelService.GetAllHotels();
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetHotels]" +
                $"GetHotels returned {hotels.Count} hotels");
            return hotels;
        }

        [HttpGet("{id}", Name = "GetHotel")]
        public HotelDataModel GetHotel(int id) 
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetHotel]" +
                $"GetHotel called with id {id}");
            var hotel = _hotelService.GetHotelById(id);
            if (hotel == null)
            {
                _logger.LogInformation(
                    $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetHotel]" +
                    $"GetHotel returned null");
                return null;
            }
            _logger.LogInformation(
                    $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetHotel]" +
                    $"GetHotel returned {hotel.Id}");
            
            return hotel;
        }

        [HttpPost("{id}", Name = "UpdateHotel")]
        public void UpdateHotel([FromBody] HotelDataModel hotel)
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][UpdateHotel]" +
                $"UpdateHotel called with id {hotel.Id}");
            _hotelService.UpdateHotel(hotel);
        }

        [HttpPost(Name = "AddHotel")]
        public void CreateHotel([FromBody] HotelDataModel hotel)
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][AddHotel]" +
                $"AddHotel called with id {hotel.Id}");
            _hotelService.CreateHotel(hotel);
        }

        [HttpPost("{id}", Name = "RemoveHotel")]
        public void RemoveHotel(int id)
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}]" +
                $"[RemoveHotel]RemoveHotel called with id {id}");
            _hotelService.RemoveHotel(id);
        }

        [HttpPost( Name = "Reserve")]
        public ActionResult<int> Reserve([FromBody] ReserveData data)
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][Reserve]Reserve called with hotel id " +
                $"{data.id} and room type id {data.roomTypeId}from {data.startDate} to {data.endDate}");
            return _hotelService.ReserveRoom(data.id, data.roomTypeId, data.startDate, data.endDate);
        }

        public class ReserveData
        {
            public int id { get; set; }
            public int roomTypeId { get; set; }
            public DateTime startDate { get; set; }
            public DateTime endDate { get; set; }
        }

        [HttpGet("{id}, {startDate}, {endDate}", Name = "GetInfo")]
        public IEnumerable<HotelServices.HotelRoomTypeCountData> GetInfo(int id, DateTime startDate, DateTime endDate)
        {
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetInfo]GetInfo called with hotel id " +
                $"{id} and from {startDate} to {endDate}");
            var availableRooms =  _hotelService.GetAllAvailableRooms(id,  startDate, endDate);
            _logger.LogInformation(
                $"[{DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}][GetInfo]" +
                $"GetInfo returned {availableRooms.Count} rooms");
            return availableRooms;
        }
    }

    
}