# Geranium.Modules
[![NuGet version](https://badge.fury.io/nu/Geranium.Modules.svg)](https://badge.fury.io/nu/Geranium.Modules)

Modules abstraction for unification of the bridge between other module systems and the registration abstraction.

## Usage example

Define one or multiple classes in assemlby inherit from `BaseModule` or `BaseModule<T>` in second case `T` is configuration type.

### Define modules

```C#
// assembly "data.dll"
public class DataModule : BaseModule
{
    public override void Install()
    {
        this.RegisterScoped<IRepository, MemoryRepository>(); // register implementation
    }
}

// assembly "app.dll"
public class AppModule : BaseModule
{
    protected override void SetDependencies()
    {
        this.DependsOn<DataModule>(); // setting dependency of DataModule
    }
}
```

### Installing modules

```C#
// assembly "app.dll"
public class Program
{
    private static void Main(string[] args)
    {
        // prepare bridge and others
        var builder = WebApplication.CreateBuilder(args);

        // installing modules
        // <--------------------- main part ------------------------>
        var installinfo = ModuleInstaller.Install(/* optional bridge */);

        // modules installed, register other components

        builder.Services.AddTransient<AppService,AppService>();
    }
}
```

### Result

```C#
// assembly "app.dll"
public class AppService
{
    public AppService(IRepository repository)
    {
        // repository here is 'MemoryRepository' from "data.dll", injected by your IoC container
    }
}
```

## How it works
The main idea of _these_ modules is the unification of the bridge between other module systems and the abstraction of registration.

### Installing
There is three main steps in module intalling:
* Getting assemblies: by default, all assemblies in folder parse by `Mono.Cecil` and check is `IModule` implementations exists in assembly.
* Extract `IModule`s from assemblies: by default, _all_ classes derived from `BaseModule`/`BaseModule<T>` returns from assembly, this means you can have multiple modules in one assembly.
* Install modules: sorting modules by `Toposort`, creating cfg classes, bind to `IModule` instances, and call `IModule.Install()`.

### Bridge
You can pass `IServiceBridge` implementation into `ModuleInstaller.Install` class (and all others in `ModulesInstallSettings`), which do main 'magic trick':
* Extracted `IModule`s registering in DI by `IServiceBridge.RegisterSingleton` method
* For installing, `IModule`s getting from DI by `IServiceBridge.ResolveService(moduleType)`. By this step, you can use full power of **your** DI library.
* When you call `this.Register***` methods in `Install` void of module, it call's `IServiceBridge.Register***` implementation.

## Extensions
`ModulesInstallSettings` contains six options which provide ability controlling module installing and registering components in modules. You can pass settings to `ModuleInstaller.Install`.

### IServiceBridge
Core abstraction of library: this "bridge" between your DI system/library and `Geranium.Modules`:
* `Register***` methods calling in `Module.Install()` and using for registering components implementations
* `ResolveService's` methods calling for getting `IModule` instances with fulfilled dependencies from your DI

You can pass `IServiceBridge` directly into `ModuleInstaller.Install` overloading.

### ConfigurationProviderFunc
You can pass your own `delegate object ConfigurationProviderFunc(Type moduleType, Type configType)` for provide config instances.

### AssmyblyProviderFunc and AssemblyLoadContext
You can pass your own `delegate Assembly[] AssmyblyProviderFunc(AssemblyLoadContext loadContext)` for provide `Assembly[]` from modules will be extracted, additionaly, if you want unload assemblies, you can pass your own `AssemblyLoadContext`.

### ExtractModulesFunc
You can pass your own `delegate ModuleInfo[] ExtractModulesFunc(Assembly[] assemblies)` for getting module information from assemblies.

### InstallModulesFunc
You can pass your own `delegate void InstallModulesFunc(ModuleInfo[] modules, IServiceBridge services, ConfigurationProviderFunc cfgProvider)` for installing modules, this is most important part.

### Localization
Inside `DefaultModuleInstaller.InstallModules` function couple of times calling `ILogger.Log` with success and (possibly) error messages. You can localize this messages by passing custom `INStringLocalizer<ModuleInstallingLoggerMessages>`.
When `ILogger.Log` will call, strings will be requested to `IStringLocalizer`, by key of `ModuleInstallingLoggerMessages` properties, for example: `IStringLocalizer["DependenciesNotInstalled"]`, if there is no value for key, will be request whole phrase : `IStringLocalizer["Module dependencies not installed"]`.