using System.Web;
using HiddenVila_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVila_Client.Pages.Authentication;

public partial class Login
{
    [Inject]
    public IAuthenticationService authenticationService { get; set; }
    [Inject]
    public NavigationManager navigationManager { get; set; }

    private AuthenticationDTO UserForAuthentication { get; set; } = new AuthenticationDTO();
    public bool IsProcessing { get; set; } = false;
    public bool ShowAuthenticationErrors { get; set; }
    public string Error { get; set; }

    public string ReturnUrl { get; set; }

    private async Task LoginUser()
    {
        ShowAuthenticationErrors = false;
        IsProcessing = true;
        var result = await authenticationService.Login(UserForAuthentication);
        IsProcessing = false;
        if (result.IsAuthSuccessful)
        {
            var absoluteUri = new Uri(navigationManager.Uri);
            var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
            ReturnUrl = queryParam["returnUrl"];

            if (string.IsNullOrWhiteSpace(ReturnUrl))
            {
                navigationManager.NavigateTo("/");
            }
            else
            {
                navigationManager.NavigateTo("/"+ReturnUrl);
                //navigationManager.NavigateTo(ReturnUrl);
            }
        }
        else
        {
            Error = result.ErrorMessage;
            ShowAuthenticationErrors = true;
        }
    }
}
