using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HiddenVila_Client.Pages.Authentication;

public partial class RedirectToLogin
{
    [Inject] private NavigationManager _navigationManager { get; set; }

    [CascadingParameter]
    public Task<AuthenticationState> authenticationState { get; set; }

    private bool notAuthorized { get; set; } = false;
    protected async override Task OnInitializedAsync()
    {
        var authState = await authenticationState;

        if (authState?.User?.Identity is null || 
            !authState.User.Identity.IsAuthenticated)
        {
            var returnUrl = _navigationManager.ToBaseRelativePath(_navigationManager.Uri);
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                _navigationManager.NavigateTo("login", true);
            }
            else
            {
                _navigationManager.NavigateTo($"login?returnUrl={returnUrl}", true);
            }
        }
        else
        {
            notAuthorized = true;
        }
        
       
    }
}
