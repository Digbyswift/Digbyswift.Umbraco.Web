# Digbyswift.Umbraco.Web

[![NuGet version (Digbyswift.Umbraco.Web)](https://img.shields.io/nuget/v/Digbyswift.Umbraco.Web.svg)](https://www.nuget.org/packages/Digbyswift.Umbraco.Web/)
[![Build and publish package](https://github.com/Digbyswift/Digbyswift.Umbraco.Web/actions/workflows/dotnet-build-publish.yml/badge.svg)](https://github.com/Digbyswift/Digbyswift.Umbraco.Web/actions/workflows/dotnet-build-publish.yml)

A nullable-enabled library of useful classes and extensions for supporting an Umbraco v10+ project.

## Controllers

A set of base controllers (and supporting classes) that expect an aggregate of dependencies:

```
public abstract class BaseSurfaceController : SurfaceController
{
    protected readonly ILogger Logger;
    protected readonly IViewRenderer ViewRenderer;

    protected BaseSurfaceController(SurfaceControllerDependencies defaultDependencies) : base(
        defaultDependencies.UmbracoContextAccessor,
        defaultDependencies.DatabaseFactory,
        defaultDependencies.Services,
        defaultDependencies.AppCaches,
        defaultDependencies.ProfilingLogger,
        defaultDependencies.PublishedUrlProvider)
    {
        Logger = defaultDependencies.Logger;
        ViewRenderer = defaultDependencies.ViewRenderer;
    }
}
```

These include: 

 - `BaseController`
 - `BaseController<T>`
 - `BaseSurfaceController`
 - `BaseSurfaceController<T>`
 - `BaseVirtualController`
 - `BaseVirtualController<T>`

Where `<T>` allows for a strongly typed content model instead of `IPublishedContent`.

There is also an extension method to register the supporting classes:

```
services.AddControllerDependencies();
```

## Extensions

A set of basic but useful extensions for making life just a little easier. These include:

### BlockListItemExtensions
  - `TypeAlias()`
  - `Is(string alias)`

### IContentExtensions
  - `GetDirtyProperties(content)`
  - `SetValueAsDocumentUdi(string alias, Guid contentKey)`
  - `SetValueAsMediaUdi(string alias, Guid contentKey)`
  - `SetValueAsMemberUdi(string alias, Guid contentKey)`
  - `SetValueAsElementUdi(string alias, Guid contentKey)`

### IContentServiceExtensions
  - `GetAllChildren(int parentId)`
  - `GetAllOfType(int contentTypeId)`

### GuidExtensions
  - `ToUdi(string entityType = uConstants.UdiEntityType.Document)`

### HttpRequestExtensions
  - `GetPreviewId()`
  - `TryGetPreviewId()`
  - `IsPreviewPath()`
  - `IsReservedPath()`
  - `IsMediaPath()`

### LinkExtensions
  - `TargetAsAttribute()`

### IMemberExtensions
  - `GetDirtyProperties()`
  - `ToIdentityUser(string memberTypeAlias, bool isApproved = true)`

 ### PublishedContentExtensions
  - `TypeAlias()`
  - `Is(string alias)`
  - `IsAny(params string[] alias)`
  - `HasTemplate()`
  - `HasAncestor(string docTypeAlias)`
  - `FirstSibling(content)`
  - `FirstSibling(string alias)`
  - `FirstSibling<T>(content)`
  - `PreviousSibling(content)`
  - `PreviousSibling(string alias)`
  - `PreviousSibling<T>(Func<T, bool>? filter = null)`
  - `LastSibling(content)`
  - `LastSibling(string alias)`
  - `LastSibling<T>(content)`
  - `NextSibling(Func<IPublishedbool>? filter = null)`
  - `NextSibling(string alias)`
  - `NextSibling<T>(Func<T, bool>? filter = null)`

### PublishedElementExtensions
 - `TypeAlias()`
 - `Is(string alias)`
 - `IsAny(params string[] alias)`

### UdiExtensions
 - `ToGuid()`
