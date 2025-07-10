# DaffittTech Network Status

## Introduction
This NuGet package is a simple Blazor utility for checking Internet connectivity. It uses the JSInterop to communicate with JavaScript functions that do the connection status workload and then relay the results to the NetworkConnection component. Its use is described under the MIT License.

## Getting Started
This README file is relevant to this NuGet package's current version, so older versions may vary. To take advantage of this Blazor utility package, follow the instructions below.

## Setting It Up
There are several steps to set up and use DaffittTech.NetworkStatus package.
>- Install the NuGet package
>- Register the package
>- Add the directive to the _Imports.razor file
>- Inject the NetworkService into your Blazor code
>- Add the NetworkConnection component  to your UI

### Install NuGet Package
Depending on your chosen Blazor project configuration, you will likely need to add this package to the application's Client-side (UI) and server-side based on where your pages and components are running from. However, you may need to experiment, as Microsoft likes to change technologies on a moment-to-moment basis.

1.	The most straightforward way to add this service to your Blazor application is to use the Package Manager in Visual Studio. Search for Daffitt Technologies and select DaffittTech.NetworkStatus package, choose the projects you wish to install it into, and click install.

2.	You can also go to [https://www.nuget.org/packages/DaffittTech.NetworkStatus](https://www.nuget.org/packages/DaffittTech.NetworkStatus) and follow the directions for your favorite method of installing NuGet packages.

### Register The NetworkService
Add the using statement and the scoped service to the Programs.cs file.
```csharp
using DaffittTech.NetworkStatus;

builder.Services.AddScoped<NetworkService>();
```

### _Imports.razor
You'll need to add this line to the ```_Imports.razor``` file where you'll use these packages.
```html
@using DaffittTech.NetworkStatus
```
If you separate your *.razor* page from its *.cs* page, you may also need to add the line above to the using section in the *.cs* page (without the "@" symbol).

### Inject NetworkService Into Your Blazor Code
If you keep your *.razor* code and backend *.cs* code together in the same razor file, add this line at the top of the page/component.
```html
@inject NetworkService NetworkService
```
If you separate your *.razor* page from its *.cs* page, add this line to the using section of the page. You'll also want to add a private property to hold the value of the network status.

```csharp
[Inject] protected NetworkService NetworkService { get; set; }

private bool NetworkStatus { get; set; } = true
```

## Using NetworkStatus
To use the component, just add it as an HTML element to the ```razor``` page.
```html
<NetworkConnection Status=@ConnectionStatus />
```
Then, an event handler will be set up to capture the action event of the network status change. You'll also need to trigger updates, respond to the change using the OnAfterRenderAsync and UpdateNetworkStatus methods, and finally call a dispose to clean it up.
```csharp
protected async override Task OnInitializedAsync()
{
NetworkService.OnNetworkStatusChanged += UpdateNetworkStatus;
await base.OnInitializedAsync();
}

protected async override Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)
    {
        await NetworkService.CheckStatusAsync();
    }
    await base.OnAfterRenderAsync(firstRender);
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
### Add NetworkConnection Component
Finally, add the NetworkConnection component to the UI.
```html
<NetworkConnection Interval="@Interval" />
```

## Software dependencies
>- This utility is built on the .Net 8.0 framework
>- Microsoft.ASPNetCore.Components.Web (>= 8.0.6)

## Latest releases
The current release version is 1.4.0

## API references
At this time, there are no API references on which to speak.

### Sample Screen Shot
![Sample Screen Shot](https://github.com/DaffittTech/DaffittTech.NetworkStatus/blob/main/DaffittTech.NetworkStatus/Sample.png?raw=true)
