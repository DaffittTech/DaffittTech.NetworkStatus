using Microsoft.JSInterop;

namespace DaffittTech.NetworkStatus
{
    public class NetworkService : IDisposable
    {
        public event Action<bool> OnNetworkStatusChanged;

        private readonly IJSRuntime _jsRuntime;
        private DotNetObjectReference<NetworkService> _dotNetReference;

        public NetworkService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _dotNetReference = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// Initializes the operation by passing the this service as a DotNet Object to the initialize function.
        /// </summary>
        /// <param name="_dotNetReference"></param>
        /// <returns>Completed Task</returns>
        public Task Initialize()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.initialize", _dotNetReference);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Monitors the online/offline status by fetching a 204 responce from a Google.com url.
        /// The "seconds" parameter is the number of seconds between each interval of the fetch.
        /// The default value is null seconds if no value is given. A value of null will disable the monitor.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>Completed Task</returns>
        public Task MonitorStatus(int? interval = null, double? timeout = null)
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.monitorStatus", interval, timeout);
            return Task.CompletedTask;
        }

        /// <summary>
        /// The CheckStatusAsync method is an asynchronous method that checks the online/offline connection status
        /// by fetching a 204 responce from a Google.com url.
        /// </summary>
        /// <returns>true (Online) or false (Offline) to the component's Action Event Handler.</returns>
        public async Task CheckStatusAsync(double? timeout = null)
        {
            var result = await _jsRuntime.InvokeAsync<bool>("networkStatus.checkStatus", timeout);
            OnNetworkStatusChanged.Invoke(result);
            await Task.CompletedTask;
        }

        /// <summary>
        /// The NotifyNetworkStatusChange is the JavaScript Invokable Interop response from the JavaScrript funcion
        /// that notifies the Blazor component/page via the OnNetworkStatusChange Action,
        /// that the status online/offline value has changed.
        /// </summary>
        /// <param name="onlineStatus"></param>
        /// <returns>true (Online) or false (Offline) to the component's Action Event Handler.</returns>
        [JSInvokable]
        public void NotifyNetworkStatusChanged(bool onlineStatus)
        {
            OnNetworkStatusChanged.Invoke(onlineStatus);
        }

        /// <summary>
        /// The Dispose method calls the JavaScript's dispose function, which stops the MonitorStatus interval
        /// and disposes anything else that needs cleaned up.
        /// </summary>
        /// <returns>Void</returns>
        public void Dispose()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.dispose");
            _dotNetReference?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
