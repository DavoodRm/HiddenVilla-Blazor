using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository
{
    public class HotelRoomRepository : IHotelRoomRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public HotelRoomRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<HotelRoomDto> CreateHotelRoom(HotelRoomDto model)
        {
            var hotel = _mapper.Map<HotelRoom>(model);
            hotel.CreatedDate = DateTime.Now;
            hotel.CreatedBy = "";
            var result = await _db.HotelRooms.AddAsync(hotel);
            await _db.SaveChangesAsync();
            return _mapper.Map<HotelRoomDto>(result.Entity);
        }

        public async Task<HotelRoomDto> UpdateHotelRoom(int roomId, HotelRoomDto model)
        {
            try
            {
                if (roomId == model.Id)
                {
                    //valid
                    HotelRoom roomDetails = await _db.HotelRooms.FindAsync(roomId);
                    HotelRoom room = _mapper.Map<HotelRoomDto, HotelRoom>(model, roomDetails);
                    room.UpdatedBy = "";
                    room.UpdatedDate = DateTime.Now;
                    var updatedRoom = _db.HotelRooms.Update(room);
                    await _db.SaveChangesAsync();
                    return _mapper.Map<HotelRoom, HotelRoomDto>(updatedRoom.Entity);
                }
                else
                {
                    //invalid
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<int> DeleteHotelRoom(int id)
        {
            var roomDetails = await _db.HotelRooms.FindAsync(id);
            if (roomDetails != null)
            {
                var allImages = await _db.HotelRoomImages.Where(p => p.RoomId == id).ToListAsync();

                _db.HotelRoomImages.RemoveRange(allImages);
                _db.HotelRooms.Remove(roomDetails);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<HotelRoomDto> GetHotelRoom(int id, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                var hotel = await _db.HotelRooms.Include(p => p.HotelRoomImages)
                    .FirstOrDefaultAsync(p => p.Id == id);
                var result = _mapper.Map<HotelRoomDto>(hotel);

                if (!string.IsNullOrWhiteSpace(checkInDateStr) && !string.IsNullOrWhiteSpace(checkOutDateStr))
                {
                    result.IsBooked = await IsRoomBooked(id, checkInDateStr, checkOutDateStr);
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<HotelRoomDto>> GetAllHotelRoom(string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                IEnumerable<HotelRoom> hotels = _db.HotelRooms
                    .Include(p => p.HotelRoomImages);
                var result = _mapper.Map<IEnumerable<HotelRoomDto>>(hotels);
                if (!string.IsNullOrWhiteSpace(checkInDateStr) &&
                    !string.IsNullOrWhiteSpace(checkOutDateStr) )
                {
                    foreach (var item in result)
                    {
                        item.IsBooked = await IsRoomBooked(item.Id, checkInDateStr, checkOutDateStr);

                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> IsRoomBooked(int roomId, string checkInDateStr, string checkOutDateStr)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(checkInDateStr) && !string.IsNullOrWhiteSpace(checkOutDateStr))
                {
                    DateTime checkInDate = DateTime.ParseExact(checkInDateStr, "MM/dd/yyyy", null);
                    DateTime checkOutDate = DateTime.ParseExact(checkOutDateStr, "MM/dd/yyyy", null);

                    var existBook = await _db.RoomOrderDetails
                        .Where(p => p.RoomId == roomId && p.IsPaymentSuccessful &&
                                    (
                                        (checkInDate < p.CheckOutDate && checkInDate.Date >= p.CheckInDate)
                                         || (checkOutDate.Date > p.CheckInDate.Date && checkInDate.Date <= p.CheckInDate.Date)
                                    )
                        ).FirstOrDefaultAsync();

                    if (existBook != null)
                    {
                        return true;
                    }

                    return false;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            return false;

        }

        public async Task<HotelRoomDto> IsRoomUnique(string name, int roomId = 0)
        {
            try
            {
                if (roomId == 0)
                {
                    var hotel = await _db.HotelRooms.FirstOrDefaultAsync(p => p.Name.ToLower() == name);
                    var result = _mapper.Map<HotelRoomDto>(hotel);

                    return result;
                }
                else
                {
                    var hotel = await _db.HotelRooms.FirstOrDefaultAsync(p => p.Name.ToLower() == name
                                                                              && p.Id != roomId);
                    var result = _mapper.Map<HotelRoomDto>(hotel);

                    return result;
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
