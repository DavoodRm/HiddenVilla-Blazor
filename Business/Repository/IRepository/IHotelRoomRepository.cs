using Models;

namespace Business.Repository.IRepository
{
    public interface IHotelRoomRepository
    {
        Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto model);
        Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto model);
        Task<int> DeleteHotelRoom(int id);
        Task<HotelRoomDto> GetHotelRoom(int id, string checkInDate = null, string checkOutDate = null);
        Task<IEnumerable<HotelRoomDto>> GetAllHotelRoom(string checkInDate=null,string checkOutDate=null);
        Task<HotelRoomDto> IsRoomUnique(string name,int roomId=0);
        public Task<bool> IsRoomBooked(int roomId, string checkInDate, string checkOutDate);

    }
}