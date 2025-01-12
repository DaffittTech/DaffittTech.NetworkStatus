# DaffittTech.NetworkStatus

## Introduction
This NuGet package is a simple Blazor utility for checking Internet connectivity. Its use is described under the MIT License.

## Getting Started
To take advantage of this Blazor utility package, follow the instructions below.

### Install NuGet Package
Depending on your chosen Blazor project configuration, you should only need to add this package to the application's Client-side (UI). However, you may need to experiment, as Microsoft likes to change technologies on a moment-to-moment basis.
1. The simplest way to add this service to your Blazor application is to use the Package Manager in Visual Studio. Search for Daffitt Technologies and select DaffittTech.NetworkStatus package, and click install.

2. You can also go to [https://www.nuget.org/profiles/DaffittTechnologies](https://www.nuget.org/profiles/DaffittTechnologies) and follow the directions for your favorite method of installing NuGet packages.

### _Imports.razor
2. You'll need to add this line to the ```_Imports.razor``` file where you'll be using these package.
```html
@using DaffittTech.NetworkStatus
```
If you separate your *.razor* page from its *.cs* page, you may also need to add one, or a combination of these lines to the using statements in the *.cs* page (without the "@" symbol).

### Using NetworkStatus
3. To use the component just add it as an HTML element to the ```razor``` page like so...
```html
<ConnectionStatus Status=@ConnectionStatus />
```

## Software dependencies
>- This utility is built on the .Net 8.0 framework
>- Microsoft.ASPNetCore.Components.Web (>= 7.0.0)

## Latest releases
The current release version is 1.2.0

## API references
At this time, there are no API references on which to speak.
