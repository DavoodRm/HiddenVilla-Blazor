
using System.Net.Http.Json;
using HiddenVila_Client.Service.IService;
using Models;
using Newtonsoft.Json;

namespace HiddenVila_Client.Service;

public class HotelRoomService : IHotelRoomService
{
    private readonly HttpClient _client;

    public HotelRoomService(HttpClient client)
    {
        _client = client;
    }
    public async Task<IEnumerable<HotelRoomDto>> GetHotelRooms(string checkInDate, string checkOutDate)
    {
        var result =
            await _client.GetFromJsonAsync<IEnumerable<HotelRoomDto>>(
                $"api/hotelroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
        return result;

        //var response = await _client.GetAsync($"api/hotelroom?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
        //var content = await response.Content.ReadAsStringAsync();
        //var rooms = JsonConvert.DeserializeObject<IEnumerable<HotelRoomDto>>(content);
        //return rooms;
    }

    public async Task<HotelRoomDto> GetHotelRoomDetials(int roomId, string checkInDate, string checkOutDate)
    {

        //var result =
        //    await _client.GetFromJsonAsync<HotelRoomDto>(
        //        $"api/hotelroom/{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
        //return result;

        var response =
            await _client.GetAsync(
                $"api/hotelroom/{roomId}?checkInDate={checkInDate}&checkOutDate={checkOutDate}");
        if (response.IsSuccessStatusCode)
        {
            var content=await response.Content.ReadAsStringAsync();
            var room = JsonConvert.DeserializeObject<HotelRoomDto>(content);
            return room;
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            var errormodel = JsonConvert.DeserializeObject<ErrorModel>(content);
            throw new Exception(errormodel.ErrorMessage);
        }

    }

}