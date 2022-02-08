using Models;

namespace HiddenVila_Client.Service.IService;

public interface IHotelRoomService
{
    public Task<IEnumerable<HotelRoomDto>> GetHotelRooms(string checkInDate, string checkOutDate);
    public Task<HotelRoomDto> GetHotelRoomDetials(int roomId, string checkInDate, string checkOutDate);
}