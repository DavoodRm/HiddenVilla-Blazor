using AutoMapper;
using DataAccess.Data;
using Models;

namespace Business.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDto, HotelRoom>();
            CreateMap<HotelRoom, HotelRoomDto>();
            CreateMap<HotelAmenity, HotelAmenityDTO>().ReverseMap();

            CreateMap<HotelRoomImage, HotelRoomImageDTO>().ReverseMap();

            CreateMap<RoomOrderDetails, RoomOrderDetailsDTO>()
                .ForMember(p => p.HotelRoomDTO, o => o.MapFrom(c => c.HotelRoom));

            CreateMap<RoomOrderDetailsDTO, RoomOrderDetails>();

        }
    }
}
