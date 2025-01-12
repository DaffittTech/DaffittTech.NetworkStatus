using Microsoft.JSInterop;

namespace DaffittTech.NetworkStatus
{
    public class NetworkService : IDisposable
    {
        private readonly IJSRuntime _jsRuntime;
        private DotNetObjectReference<NetworkService> _dotNetReference;

        public event Action<bool> OnNetworkStatusChanged;

        public NetworkService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _dotNetReference = DotNetObjectReference.Create(this);
        }

        /// <summary>
        /// Initializes the operation by passing the this service as a DotNet Object to the initialize function.
        /// </summary>
        /// <returns>Completed Task</returns>
        public Task Initialize()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.initialize", _dotNetReference);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Monitors the online/offline status by fetching a 204 responce from a Google.com url.
        /// The "seconds" parameter is the number of seconds between each interval of the fetch.
        /// The default value is 60 seconds if no value is given. A value of 999 or greater will ignore this method.
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns>Completed Task</returns>
        public Task MonitorStatus(int? seconds = null)
        {
            if (seconds is not null)
            {
                _jsRuntime.InvokeVoidAsync("networkStatus.monitorStatus", seconds);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// The FetchStatus method does just what it sounds like; it fetches the online/offline connection 
        /// status by fetching a 204 responce from a Google.com url.
        /// </summary>
        /// <returns>Completed Task</returns>
        public Task FetchStatus()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.fetchStatus");
            return Task.CompletedTask;
        }

        /// <summary>
        /// The NotifyNetworkStatusChange is the JavaScript Invokable Interop response from the JavaScrript funcion
        /// that notifies the Blazor component/page via the OnNetworkStatusChange Action,
        /// that the status online/offline value has changed.
        /// </summary>
        /// <param name="onlineStatus"></param>
        /// <returns>Calls the component's Event Handler and passes either "Online" or "Offline" to that handler.</returns>
        [JSInvokable]
        public void NotifyNetworkStatusChanged(bool onlineStatus)
        {
            OnNetworkStatusChanged.Invoke(onlineStatus);
        }

        /// <summary>
        /// The Dispose() method calls the JavaScript's dispose function, which stops the MonitorStatus interval.
        /// </summary>
        /// <returns>Void</returns>
        public void Dispose()
        {
            _jsRuntime.InvokeVoidAsync("networkStatus.dispose");
        }
    }
}
