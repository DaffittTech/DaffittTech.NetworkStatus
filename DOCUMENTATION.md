# DaffittTech.NetworkStatus Documentation

## Overview
**DaffittTech.NetworkStatus** is a Blazor utility package for monitoring Internet connectivity in Blazor applications. It leverages JSInterop to interact with browser APIs and provides a simple component and service for real-time network status updates.

- **Target Framework:** .NET 9.0
- **NuGet Package:** [DaffittTech.NetworkStatus](https://www.nuget.org/packages/DaffittTech.NetworkStatus)
- **License:** MIT

## Features
- Detects online/offline status in real time.
- Blazor component for UI integration.
- Service for programmatic access to network status.
- Customizable check intervals.
- Easy integration with Blazor projects.

## Installation

1. **Via NuGet Package Manager:**
   - Search for `DaffittTech.NetworkStatus` and install into your Blazor project.

2. **Via .NET CLI:**

## Setup

### 1. Register the Service
Add the following to your `Program.cs`:

### 2. Update _Imports.razor
Add:
```razor
@using DaffittTech.NetworkStatus
```
### 3. Inject the Service
In your `.razor` file:
```razor
@inject NetworkStatusService NetworkStatusService
```
Or in code-behind:

### 4. Add the Component
Place the component in your UI:
```razor
<NetworkStatus />
```
That's it! The component will automatically display the current network status and update in real-time.

## Usage

### Component Parameters
- **CheckInterval**: The interval (in milliseconds) to check the network status. Default is `5000` (5 seconds).
- **StatusChanged**: EventCallback that triggers when the network status changes.

### Service Methods
- **GetStatusAsync()**: Returns the current network status.
- **StartMonitoring()**: Starts the network status monitoring.
- **StopMonitoring()**: Stops the network status monitoring.

## Troubleshooting
- Ensure that your Blazor application is correctly set up to use JavaScript interop.
- Check the browser's console for any errors related to the network status scripts.
- If the component does not display the status, ensure that it is correctly placed in the UI and that the service is registered.

## Contributing
We welcome contributions to **DaffittTech.NetworkStatus**. Please follow these steps:
1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Make the necessary changes and commit them.
4. Push your branch and submit a pull request.

## License
**DaffittTech.NetworkStatus** is licensed under the MIT License. See the LICENSE file for more information.

## Acknowledgements
- Inspired by the need for reliable network status monitoring in web applications.
- Uses Blazor and JSInterop for seamless integration and performance.

## Contact
- `Interval` (optional): Milliseconds between status checks.
- `Timeout` (optional): Timeout for status check.

## Usage Example
```HTML
@page "/network-status" @inject NetworkService NetworkService @implements IDisposable

<div> <NetworkConnection Interval="5000" /> <p>Network Status: @(NetworkStatus ? "Online" : "Offline")</p> </div>
```

```c#
    private bool NetworkStatus { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        NetworkService.OnNetworkStatusChanged += UpdateNetworkStatus;
        await base.OnInitializedAsync();
    }

    private void UpdateNetworkStatus(bool status)
    {
        NetworkStatus = status;
        StateHasChanged();
    }

    public void Dispose()
    {
        NetworkService.OnNetworkStatusChanged -= UpdateNetworkStatus;
        NetworkService.Dispose();
    }
```

## Dependencies
- [.NET 9.0](https://dotnet.microsoft.com/)
- [Microsoft.AspNetCore.Components.Web (>= 8.0.6)](https://www.nuget.org/packages/Microsoft.AspNetCore.Components.Web)

## Release Notes
- **2.0.0:** Simplified features and improved stability.

## Repository
- [GitHub: DaffittTech.NetworkStatus](https://github.com/DaffittTech/DaffittTech.NetworkStatus)

## License
This project is licensed under the MIT License. See `LICENSE.txt` for details.

## Screenshots
![Sample Screen Shot](https://github.com/DaffittTech/DaffittTech.NetworkStatus/blob/main/DaffittTech.NetworkStatus/Sample.png?raw=true)
