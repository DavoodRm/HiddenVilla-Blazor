using Microsoft.JSInterop;

namespace HiddenVila_Server.Helper
{
    public static class IJSRuntimeExtension
    {
        public static async ValueTask ToastrSuccess(this IJSRuntime jsRuntime,string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "success", message );

        }
        public static async ValueTask ToastrError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("ShowToastr", "error", message);

        }
        public static async ValueTask SweetAlertSuccess(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("SweetAlert", "success", message);
        }

        public static async ValueTask SweetAlertError(this IJSRuntime jsRuntime, string message)
        {
            await jsRuntime.InvokeVoidAsync("SweetAlert", "error", message);
        }

        public static async ValueTask ShowDeleteConfirmationModal(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("ShowDeleteConfirmationModal");
        }
        public static async ValueTask HideDeleteConfirmationModal(this IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("HideDeleteConfirmationModal");
        }
    }
}
