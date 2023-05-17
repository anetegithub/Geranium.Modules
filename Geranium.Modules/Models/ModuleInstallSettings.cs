using Geranium.Modules.DefaultFunctions;
using Geranium.Modules.Functions;
using Geranium.Modules.Implementations;
using Geranium.Modules.Interfaces;
using Geranium.Modules.Localization;
using Geranium.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;

namespace Geranium.Modules.Models
{
    /// <summary>
    /// Modules installing settings
    /// </summary>
    public class ModulesInstallSettings
    {
        /// <summary>
        /// Create new instance of settings with custom values, <see cref="IServiceBridge"/> most important.
        /// </summary>
        /// <param name="containerBridge"></param>
        /// <param name="assmyblyProviderFunc"></param>
        /// <param name="extractModulesFunc"></param>
        /// <param name="installModulesFunc"></param>
        /// <param name="ctx"></param>
        public ModulesInstallSettings(IServiceBridge services = default,
            ConfigurationProviderFunc configurationProviderFunc = default,
            AssmyblyProviderFunc assmyblyProviderFunc = default,
            ExtractModulesFunc extractModulesFunc = default,
            InstallModulesFunc installModulesFunc = default,
            AssemblyLoadContext ctx = default,
            IStringLocalizer<ModuleInstallingLoggerMessages> localizer = default)
        {
            if (ctx!=default)
                AssemblyLoadContext = ctx;

            if (configurationProviderFunc!=default)
                GetConfigFunc = configurationProviderFunc;

            if (assmyblyProviderFunc!=default)
                GetAssembliesFunc = assmyblyProviderFunc;

            if (services!=default)
                Services = services;

            if (extractModulesFunc!=default)
                ExtractFunc = extractModulesFunc;

            if (installModulesFunc!=default)
                InstallFunc = installModulesFunc;

            if (localizer!=default)
                Localizer= localizer;
        }

        /// <summary>
        /// Default value <see cref="ServiceCollectionBridge"/>
        /// </summary>
        public IServiceBridge Services { get; } = new ServiceCollectionBridge();

        /// <summary>
        /// Default value <see cref="AssemblyFromFiles.GetFiles(AssemblyLoadContext)"/>
        /// </summary>
        public AssmyblyProviderFunc GetAssembliesFunc { get; } = AssemblyFromFiles.GetFiles;

        /// <summary>
        /// Default value <see cref="DefaultModuleExtractor.ExtractModules(System.Reflection.Assembly[])"/>
        /// </summary>
        public ExtractModulesFunc ExtractFunc { get; } = DefaultModuleExtractor.ExtractModules;

        public IStringLocalizer<ModuleInstallingLoggerMessages> Localizer { get; } = default;

        /// <summary>
        /// Default value <see cref="DefaultConfigFunc.GetConfig(Type,Type)"/>
        /// </summary>
        public ConfigurationProviderFunc GetConfigFunc { get; } = DefaultConfigFunc.GetConfig;

        /// <summary>
        /// Default value <see cref="DefaultModuleInstaller.InstallModules(ModuleInfo[], IServiceBridge, ConfigurationProviderFunc)"/>
        /// </summary>
        public InstallModulesFunc InstallFunc { get; } = DefaultModuleInstaller.InstallModules;

        /// <summary>
        /// Default value <see cref="AssemblyLoadContext.Default"/>
        /// </summary>
        public AssemblyLoadContext AssemblyLoadContext { get; } = AssemblyLoadContext.Default;

        /// <summary>
        /// Check all property values for null
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void Validate()
        {
            List<string> nullProps = new List<string>();

            var propNames = this.GetType().GetProperties().Select(x => x.Name);

            foreach (var propName in propNames)
            {
                if (propName==nameof(Localizer))
                    continue;

                var value = this.GetPropValue(propName);
                if (value == null)
                    nullProps.Add(propName);
            }

            if (nullProps.Count > 0)
                throw new ArgumentException($"{string.Join(",", nullProps)} properties are null!");
        }
    }
}