using Geranium.Modules.Interfaces;
using Geranium.Modules.Localization;
using Geranium.Modules.Models;
using Microsoft.Extensions.Localization;
using System;

namespace Geranium.Modules.Functions
{
    /// <summary>
    /// Function installing modules by infoes and bridge
    /// </summary>
    /// <param name="modules"></param>
    /// <param name="container"></param>
    public delegate void InstallModulesFunc(ModuleInfo[] modules, IServiceBridge services, ConfigurationProviderFunc cfgProvider, IStringLocalizer<ModuleInstallingLoggerMessages> localizer);
}
