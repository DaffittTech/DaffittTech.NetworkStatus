# DaffittTech.NetworkStatus

## Introduction
This NuGet package is a simple Blazor utility to check Internet connectivity. Use of this package is under the MIT License.

## Getting Started
To take advantage of this Blazor utility package follow the instructions that follow.

### Install NuGet Package
1. Depending on your chosen Blazor project configuration, you should only need to add this package to the Client-side (UI) of the application. However, you may need to experiment as Microsoft likes to change technologies on a moment to moment basis.

    a. There are a few ways to add this service to your own Blazor application. The simplest way is to use the Package Manager in Visual Studio.

    b. You can also go to [https://www.nuget.org/packages/DaffittTech.Blazor.Components](https://www.nuget.org/packages/DaffittTech.Blazor.Components) and follow the directions for your favorite method of installing NuGet packages. 

### _Imports.razor
2. You'll need to add this lines to the ```_Imports.razor``` file where you'll be using these package.
```csharp
@using DaffittTech.NetworkStatus
```
If you separate your *.razor* page from its *.cs* page, you may also need to add one, or a combination of these lines to the using statements in the *.cs* page (without the "@" symbol).

### Using NetworkStatus
3. To use the component just add it as an HTML element to the ```razor``` page like so...
```html
<AccordionComponent Title="Accordion Title" AccordionPageUrl="index">
    <div>
        <h4>Accordion Content</h4>
        <p>
            This is some fake content for the accordion component.
            Add your own stuff as you please.
        </p>
    </div>
</AccordionComponent>
```

## Software dependencies
>- This utility is built on the .Net 8.0 framework
>- Microsoft.ASPNetCore.Components.Web (>= 7.0.0)

## Latest releases
The current release version is 1.0.0

## API references
At this time there are no API references of which to speak.
