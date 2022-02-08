using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Business.Repository;

public class HotelImagesRepository:IHotelImagesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public HotelImagesRepository(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<int> CreateHotelRoomImage(HotelRoomImageDTO model)
    {
        var image = _mapper.Map<HotelRoomImage>(model);
        await _db.HotelRoomImages.AddAsync(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelRoomImageByImageId(int id)
    {
        var image = await _db.HotelRoomImages.FindAsync(id);
        _db.HotelRoomImages.Remove(image);
        return await _db.SaveChangesAsync();
    }
    public async Task<int> DeleteHotelRoomImageByRoomId(int id)
    {
        var image = await _db.HotelRoomImages.Where(p=>p.RoomId==id).ToListAsync();
        _db.HotelRoomImages.RemoveRange(image);
        return await _db.SaveChangesAsync();
    }

    public async Task<int> DeleteHotelImageByImageUrl(string imageUrl)
    {
        var allImages = await _db.HotelRoomImages
            .FirstOrDefaultAsync(p => p.RoomImageUrl.ToLower() == imageUrl.ToLower());
        if (allImages == null)
            return 0;
          _db.HotelRoomImages.Remove(allImages);
          return await _db.SaveChangesAsync();
    }

    public Task<IEnumerable<HotelRoomImageDTO>> GetHotelRoomImages(int roomId)
    {
        throw new NotImplementedException();
    }

    public async  Task<IEnumerable<HotelRoomImageDTO>> GetAllHotelRoomImages(int roomId)
    {
        return _mapper.Map<IEnumerable<HotelRoomImage>, IEnumerable<HotelRoomImageDTO>>(
            await _db.HotelRoomImages.Where(x => x.RoomId == roomId).ToListAsync());
    }


}