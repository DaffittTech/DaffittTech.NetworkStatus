using Microsoft.AspNetCore.Components;

namespace DaffittTech.NetworkStatus
{
    public partial class NetworkConnection : ComponentBase
    {
        [Inject] protected NetworkService NetworkService { get; set; }
        [Parameter] public int? Interval { get; set; } = null; // timing between checks in seconds.
        [Parameter] public double? Timeout { get; set; } = null; // time allowed forchecking status

        private string ConnectionStatus { get; set; } = "Checking...";
        private bool NetworkStatus { get; set; } = false;
        private string AlertColor { get; set; } = "alert-secondary";

        protected async override Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                NetworkService.OnNetworkStatusChanged += UpdateNetworkStatus;
                await NetworkService.Initialize();
                await NetworkService.MonitorStatus(Interval, Timeout);
                await NetworkService.CheckStatusAsync(Timeout);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private void UpdateNetworkStatus(bool status)
        {
            NetworkStatus = status;
            ConnectionStatus = NetworkStatus == true ? "Online" : "Offline";
            ConnectionStatus = string.IsNullOrEmpty(ConnectionStatus) || ConnectionStatus.ToLower().Contains("checking") ? "Checking..." : ConnectionStatus.ToLower() == "online" ? "Online" : "Offline";
            AlertColor = string.IsNullOrEmpty(ConnectionStatus) || ConnectionStatus.Contains("checking", StringComparison.CurrentCultureIgnoreCase) ? "alert-secondary" : ConnectionStatus.Contains("online", StringComparison.CurrentCultureIgnoreCase) ? "alert-success" : "alert-danger";
            StateHasChanged();
        }
    }
}
