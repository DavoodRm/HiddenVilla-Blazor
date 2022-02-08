using HiddenVila_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVila_Client.Pages.Authentication;

public partial class Register
{
    [Inject] 
    public IAuthenticationService authenticationService { get; set; }
    [Inject] 
    public NavigationManager navigationManager { get; set; }

    private UserRequestDTO UserForRegisteration { get; set; } = new UserRequestDTO();
    public bool IsProcessing { get; set; } = false;
    public bool ShowRegistrationErrors { get; set; }
    public IEnumerable<string> Errors { get; set; }

    private async Task RegisterUser()
    {
        ShowRegistrationErrors = false;
        IsProcessing = true;
        var result = await authenticationService.RegisterUser(UserForRegisteration);
        IsProcessing = false;
        if (result.IsRegisterationSuccessful)
        {

            navigationManager.NavigateTo("/login");
        }
        else
        {
            Errors = result.Errors;
            ShowRegistrationErrors = true;
        }
    }
}
