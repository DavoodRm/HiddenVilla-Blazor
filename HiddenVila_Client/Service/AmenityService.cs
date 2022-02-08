using System.Net.Http.Json;
using HiddenVila_Client.Service.IService;
using Models;

namespace HiddenVila_Client.Service;

public class AmenityService : IAmenityService
{
    private readonly HttpClient _client;

    public AmenityService(HttpClient client)
    {
        _client = client;
    }
    public async Task<IEnumerable<HotelAmenityDTO>> GetAmenities() 
    {
        var result =
            await _client.GetFromJsonAsync<IEnumerable<HotelAmenityDTO>>($"api/hotelamenity");
        return result;
    }
}