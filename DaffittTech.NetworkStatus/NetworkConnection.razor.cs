using Microsoft.AspNetCore.Components;

namespace DaffittTech.NetworkStatus
{
    /// <summary>
    /// Represents a Blazor component that monitors and displays the network connection status.
    /// </summary>
    public partial class NetworkConnection : ComponentBase
    {
        /// <summary>
        /// Gets or sets the <see cref="NetworkService"/> used to monitor network status.
        /// </summary>
        [Inject] protected NetworkService NetworkService { get; set; }

        /// <summary>
        /// Gets or sets the timing between network status checks in seconds.
        /// </summary>
        [Parameter] public int? Interval { get; set; } = null; // timing between checks in seconds.

        /// <summary>
        /// Gets or sets the time allowed for checking network status, in seconds.
        /// </summary>
        [Parameter] public double? Timeout { get; set; } = null; // time allowed forchecking status

        private string ConnectionStatus { get; set; } = "Checking...";
        private bool NetworkStatus { get; set; } = false;
        private string AlertColor { get; set; } = "alert-secondary";

        /// <summary>
        /// Called by the framework after the component has rendered.
        /// If this is the first render, subscribes to network status changes and starts monitoring.
        /// </summary>
        /// <param name="firstRender">True if this is the first time the component has rendered; otherwise, false.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                NetworkService.OnNetworkStatusChanged += UpdateNetworkStatus;
                await NetworkService.Initialize();
                await NetworkService.MonitorStatus(Interval, Timeout);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        /// <summary>
        /// Updates the network status and adjusts related properties accordingly.
        /// </summary>
        /// <remarks>This method updates the <c>NetworkStatus</c>, <c>ConnectionStatus</c>, and
        /// <c>AlertColor</c> properties  based on the provided network status. It also triggers a state update by
        /// calling <c>StateHasChanged</c>.</remarks>
        /// <param name="status">A boolean value indicating the current network status.  <see langword="true"/> represents an online state,
        /// and <see langword="false"/> represents an offline state.</param>
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
