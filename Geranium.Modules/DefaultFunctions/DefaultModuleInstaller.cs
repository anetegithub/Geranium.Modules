using Geranium.Modules.Functions;
using Geranium.Modules.Interfaces;
using Geranium.Modules.Localization;
using Geranium.Modules.Models;
using Geranium.Reflection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geranium.Modules.DefaultFunctions
{
    /// <summary>
    /// Holder of default install modules function
    /// </summary>
    public static class DefaultModuleInstaller
    {
        /// <summary>
        /// Install modules by module infoes
        /// </summary>
        /// <param name="moduleInfos"></param>
        /// <param name="container"></param>
        /// <exception cref="ApplicationException"></exception>
        public static void InstallModules(ModuleInfo[] moduleInfos, IServiceBridge services, ConfigurationProviderFunc cfg, IStringLocalizer<ModuleInstallingLoggerMessages> localizer)
        {
            var msgs = LocalizedMessages(localizer);
            var logger = services.ResolveService<ILogger<IModule>>();

            var modules = RegisterModules(moduleInfos, services, cfg);

            if (modules.Count(x => x.IsRoot)==1)
            {
                Type rootType = modules.FirstOrDefault(x => x.IsRoot).GetType();
                foreach (var module in modules)
                {
                    if (module.GetType()!=rootType)
                        module.Dependencies.Add(rootType.FullName);
                }
            }

            foreach (var module in modules)
            {
                module.InitDependencies();
            }

            var sortedModules = modules.TopoSort();

            var installed = new Dictionary<string, IModule>();

            foreach (var module in sortedModules)
            {
                var dependenciesLoaded = module.Dependencies.IsEmpty() || module.Dependencies.All(installed.ContainsKey);
                if (dependenciesLoaded)
                {
                    logger?.LogDebug($"{msgs.Installing}: {module.GetType()}");
                    try
                    {
                        module.Install();
                        module.Status = Installing.InstallStatus.Installed;
                        installed[module.Id] = module;
                    }
                    catch (Exception e)
                    {
                        var errorMessage = $"{msgs.InstallingError} {module.GetType()}";
                        logger?.LogError(e, errorMessage);

                        module.Status = Installing.InstallStatus.Broken;

                        if (module.SystemRequired)
                            throw new ApplicationException(errorMessage, e);
                    }
                }
                else
                {
                    var unsatisfiedDependencies = module.Dependencies.Where(x => !installed.ContainsKey(x)).ToArray();
                    var errorMessage = $"{msgs.InstallingError} {module.GetType()}. " +
                                       $"{msgs.DependenciesNotInstalled}: {string.Join(", ", unsatisfiedDependencies.Select(x => x.GetType()))}";

                    logger?.LogError(errorMessage);

                    if (module.SystemRequired)
                        throw new ApplicationException(errorMessage);
                }
            }
        }

        private static ModuleInstallingLoggerMessages LocalizedMessages(IStringLocalizer<ModuleInstallingLoggerMessages> localizer)
        {
            if (localizer==null)
                return new ModuleInstallingLoggerMessages();

            var msg = new ModuleInstallingLoggerMessages();

            var installing = localizer[nameof(msg.Installing)] ?? localizer[msg.Installing];
            if (installing!=default)
                msg.Installing = installing;

            var error = localizer[nameof(msg.InstallingError)] ?? localizer[msg.InstallingError];
            if (error!=default)
                msg.InstallingError = error;

            var deps = localizer[nameof(msg.DependenciesNotInstalled)] ?? localizer[msg.DependenciesNotInstalled];
            if (deps!=default)
                msg.DependenciesNotInstalled = deps;

            return msg;
        }

        private static List<IModule> RegisterModules(ModuleInfo[] moduleInfoes, IServiceBridge services, ConfigurationProviderFunc cfgProvider)
        {
            var modules = new List<IModule>();

            foreach (var moduleInfo in moduleInfoes)
            {
                var moduleType = moduleInfo.ModuleType;

                var forTypes = moduleType
                    .GetInterfaces()
                    .Where(x => x!=typeof(IToposort))
                    .Concat(new Type[] { moduleType })
                    .ToArray();                

                services.RegisterSingleton(moduleType, forTypes);

                var module = moduleInfo.Module = services.ResolveService(moduleType).As<IModule>();

                if (module is IModuleConfigured moduleConfigured)
                {
                    var cfgType = moduleConfigured.GetConfigurationType();
                    var cfg = cfgProvider(moduleType, cfgType);
                    moduleConfigured.SetConfiguration(cfg);
                    moduleInfo.Configuration=new ModuleConfigInfo()
                    {
                        Type=cfgType,
                        Instance = cfg
                    };
                }

                module.Services = services;
                modules.Add(module);
            }

            return modules;
        }
    }
}