using Models;

namespace HiddenVila_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<RegisterationResponseDTO> RegisterUser(UserRequestDTO model);

        Task<AuthenticationResponseDTO> Login(AuthenticationDTO model);

        Task Logout();
    }
}
