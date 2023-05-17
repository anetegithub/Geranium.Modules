using Geranium.Modules.Interfaces;
using Geranium.Modules.Models;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace Geranium.Modules
{
    /// <summary>
    /// Entry point for Modules
    /// </summary>
    public static class ModuleInstaller
    {
        /// <summary>
        /// Installing <see cref="IModule"/>'s:
        /// <para>
        /// 0. <see cref="ModulesInstallSettings.Validate"/>
        /// </para>
        /// <para>
        /// 1. <see cref="ModulesInstallSettings.GetAssembliesFunc"/>()
        /// </para>
        /// <para>
        /// 2. <see cref="ModulesInstallSettings.ExtractFunc"/>()
        /// </para>
        /// <para>
        /// 3. <see cref="ModulesInstallSettings.InstallFunc"/>()
        /// </para>
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static ModuleInfo[] Install(IServiceBridge services)
            => Install(new ModulesInstallSettings(services));

        /// <summary>
        /// Installing <see cref="IModule"/>'s:
        /// <para>
        /// 0. <see cref="ModulesInstallSettings.Validate"/>
        /// </para>
        /// <para>
        /// 1. <see cref="ModulesInstallSettings.GetAssembliesFunc"/>()
        /// </para>
        /// <para>
        /// 2. <see cref="ModulesInstallSettings.ExtractFunc"/>()
        /// </para>
        /// <para>
        /// 3. <see cref="ModulesInstallSettings.InstallFunc"/>()
        /// </para>
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static ModuleInfo[] Install(ModulesInstallSettings settings = default)
        {
            if (settings == default)
            {
                settings = new ModulesInstallSettings();
            }
            else
            {
                settings.Validate();
            }

            var assemblies = settings.GetAssembliesFunc(settings.AssemblyLoadContext);

            var moduleInfoes = settings.ExtractFunc(assemblies);

            settings.InstallFunc(moduleInfoes, settings.Services, settings.GetConfigFunc, settings.Localizer);

            return moduleInfoes;
        }
    }
}