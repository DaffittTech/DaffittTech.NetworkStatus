using Microsoft.AspNetCore.Components;

namespace DaffittTech.NetworkStatus
{
    public partial class NetworkConnection : ComponentBase
    {
        [Inject] protected NetworkService NetworkService { get; set; }
        [Parameter] public int? Interval { get; set; } = null; // timing between checks in seconds.

        private string ConnectionStatus { get; set; } = "Checking...";
        private bool NetworkStatus { get; set; }
        private string AlertColor { get; set; } = "alert-secondary";

        protected async override Task OnParametersSetAsync()
        {
            await NetworkService.MonitorStatus(Interval);
            await base.OnParametersSetAsync();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                NetworkService.OnNetworkStatusChanged += UpdateNetworkStatus;
                await NetworkService.Initialize();
                await NetworkService.CheckStatusAsync();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private void UpdateNetworkStatus(bool status)
        {
            NetworkStatus = status;
            ConnectionStatus = NetworkStatus == true ? "Online" : "Offline";
            ConnectionStatus = string.IsNullOrEmpty(ConnectionStatus) || ConnectionStatus.ToLower().Contains("checking") ? "Checking..." : ConnectionStatus.ToLower() == "online" ? "Online" : "Offline";
            AlertColor = string.IsNullOrEmpty(ConnectionStatus) || ConnectionStatus.ToLower().Contains("checking") ? "alert-secondary" : ConnectionStatus.ToLower() == "online" ? "alert-success" : "alert-danger";
            StateHasChanged();
        }
    }
}
