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
        public IEnumerable<HotelDataModel> GetHotels() => _hotelService.GetAllHotels();

        [HttpGet("{id}", Name = "GetHotel")]
        public HotelDataModel GetHotel(int id) => _hotelService.GetHotelById(id);

        [HttpPost("{id}", Name = "UpdateHotel")]
        public void UpdateHotel([FromBody] HotelDataModel hotel)
        {
            _hotelService.UpdateHotel(hotel);
        }

        [HttpPost(Name = "AddHotel")]
        public void CreateHotel([FromBody] HotelDataModel hotel)
        {
           _hotelService.CreateHotel(hotel);
        }

        [HttpPost("{id}", Name = "RemoveHotel")]
        public void RemoveHotel(int id)
        {
            _hotelService.RemoveHotel(id);
        }

        [HttpPost( Name = "Reserve")]
        public ActionResult<int> Reserve([FromBody] ReserveData data)
        {
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
            return _hotelService.GetAllAvailableRooms(id,  startDate, endDate);
        }
    }

    
}