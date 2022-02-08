using System.Globalization;
using Business.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace HiddenVilla_Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class HotelRoomController : ControllerBase
{
    private readonly IHotelRoomRepository _hotelRoomRepository;

    public HotelRoomController(IHotelRoomRepository hotelRoomRepository)
    {
        _hotelRoomRepository = hotelRoomRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetHotelRooms(string checkInDate = null, string checkOutDate = null)
    {
        if (string.IsNullOrWhiteSpace(checkInDate) || string.IsNullOrWhiteSpace(checkOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "All parameters need to be supplies"
            });
        }

        if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dtCheckInDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "invalid CheckIn date format.valid format will bee MM/dd/yyyy"
            });
        }
        if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dtCheckOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "invalid CheckOut date format.valid format will bee MM/dd/yyyy"
            });
        }


        var allRooms = await _hotelRoomRepository.GetAllHotelRoom(checkInDate, checkOutDate);
        return Ok(allRooms);
    }

    [HttpGet("{roomId}")]
    public async Task<IActionResult> GetHotelRoom(int? roomId, string checkInDate = null, string checkOutDate = null)
    {
        if (roomId == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }
        if (string.IsNullOrWhiteSpace(checkInDate) || string.IsNullOrWhiteSpace(checkOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "All parameters need to be supplies"
            });
        }

        if (!DateTime.TryParseExact(checkInDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dtCheckInDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "invalid CheckIn date format.valid format will bee MM/dd/yyyy"
            });
        }
        if (!DateTime.TryParseExact(checkOutDate, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out var dtCheckOutDate))
        {
            return BadRequest(new ErrorModel()
            {
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = "invalid CheckOut date format.valid format will bee MM/dd/yyyy"
            });
        }

        var roomDetails = await _hotelRoomRepository.GetHotelRoom(roomId.Value, checkInDate, checkOutDate);
        if (roomDetails == null)
        {
            return BadRequest(new ErrorModel()
            {
                Title = "",
                ErrorMessage = "Invalid Room Id",
                StatusCode = StatusCodes.Status400BadRequest
            });
        }

        return Ok(roomDetails);
    }
}
