using Models;

namespace HiddenVila_Client.Service.IService;

public interface IAmenityService
{
    Task<IEnumerable<HotelAmenityDTO>> GetAmenities();
}
