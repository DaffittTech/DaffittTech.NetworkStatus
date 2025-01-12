using Microsoft.AspNetCore.Components;

namespace DaffittTech.NetworkStatus
{
    public partial class ConnectionStatus : ComponentBase
    {
        [Parameter] public string Status { get; set; } = "Checking...";
        private string AlertColor { get; set; } = "alert-secondary";

        protected override Task OnParametersSetAsync()
        {
            Status = string.IsNullOrEmpty(Status) || Status.ToLower().Contains("checking") ? "Checking..." : Status.ToLower() == "online" ? "Online" : "Offline";
            AlertColor = string.IsNullOrEmpty(Status) || Status.ToLower().Contains("checking") ? "alert-secondary" : Status.ToLower() == "online" ? "alert-success" : "alert-danger";
            return base.OnParametersSetAsync();
        }
    }
}
