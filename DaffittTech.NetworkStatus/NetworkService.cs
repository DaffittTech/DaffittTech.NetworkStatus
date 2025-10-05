using Microsoft.JSInterop;

namespace DaffittTech.NetworkStatus
{
    /// <summary>
    /// Provides network status monitoring and notification services for Blazor applications.
    /// This service enables detection of online/offline status changes and exposes events for subscribers.
    /// </summary>
    public class NetworkService : IDisposable
    {
        /// <summary>
        /// Occurs when the network status changes.
        /// The event provides a <see langword="bool"/> value indicating whether the network is online (<see langword="true"/>) or offline (<see langword="false"/>).
        /// </summary>
        public event Action<bool> OnNetworkStatusChanged;
        private readonly IJSRuntime _jsRuntime;
        private DotNetObjectReference<NetworkService> _dotNetReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkService"/> class with the specified JavaScript runtime.
        /// </summary>
        /// <param name="jsRuntime">The JavaScript runtime to use for interop operations.</param>
        public NetworkService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _dotNetReference = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// Initializes the network status monitoring system.
        /// by passing the this service as a DotNet Object to the initialize function.
        /// </summary>
        /// <remarks>This method sets up the necessary JavaScript interop for monitoring network status
        /// changes. It must be called before any network status-related functionality is used.</remarks>
        /// <returns></returns>
        public Task Initialize()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.initialize", _dotNetReference);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Monitors the network status at specified intervals and for a specified duration.
        /// by fetching a 204 responce from a Google.com url.
        /// The "seconds" parameter is the number of seconds between each interval of the fetch.
        /// The default value is null seconds if no value is given. A value of null will disable the monitor.
        /// </summary>
        /// <remarks>This method invokes a JavaScript function to monitor the network status. It does not
        /// block the calling thread and completes immediately.</remarks>
        /// <param name="interval">The interval, in milliseconds, at which the network status is checked. If <see langword="null"/>, a default
        /// interval is used.</param>
        /// <param name="timeout">The maximum duration, in milliseconds, to monitor the network status. If <see langword="null"/>, monitoring
        /// continues indefinitely.</param>
        /// <returns>A completed <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task MonitorStatus(int? interval = null, double? timeout = null)
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.monitorStatus", interval, timeout);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Checks the current network status and triggers the <see cref="OnNetworkStatusChanged"/> event with the
        /// result. The GetStatusAsync method is an asynchronous method that checks the online/offline connection status
        /// by fetching a 204 responce from a Google.com url.
        /// </summary>
        /// <remarks>This method invokes a JavaScript function to determine the network status and raises
        /// the <see cref="OnNetworkStatusChanged"/> event with the result. The event is triggered with a <see
        /// langword="true"/> value if the network is available; otherwise, <see langword="false"/>.</remarks>
        /// <param name="timeout">An optional timeout value, in milliseconds, specifying the maximum time to wait for the network status check
        /// to complete. If <see langword="null"/>, the default timeout is used.</param>
        /// <returns></returns>
        public async Task GetStatusAsync(double? timeout = null)
        {
            bool result = await _jsRuntime.InvokeAsync<bool>("networkStatus.getStatusAsync", timeout);
            OnNetworkStatusChanged.Invoke(result);
            await Task.CompletedTask;
        }

        /// <summary>
        /// Notifies the subscribed Blazor component/page via the OnNetworkStatusChange Action about a change in the network status.
        /// </summary>
        /// <param name="onlineStatus">A value indicating the current network status.  <see langword="true"/> if the network is online; otherwise,
        /// <see langword="false"/>.</param>
        [JSInvokable]
        public void NotifyNetworkStatusChanged(bool onlineStatus)
        {
            OnNetworkStatusChanged.Invoke(onlineStatus);
        }

        /// <summary>
        /// Releases the resources used by the current instance of the class.
        /// The Dispose method calls the JavaScript's dispose function, which stops the MonitorStatus interval
        /// and disposes anything else that needs cleaned up.
        /// </summary>
        /// <remarks>This method should be called when the instance is no longer needed to ensure proper
        /// cleanup of resources.  It invokes the necessary JavaScript runtime disposal logic and suppresses
        /// finalization to optimize garbage collection.</remarks>
        public void Dispose()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.dispose");
            _dotNetReference?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
