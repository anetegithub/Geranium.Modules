using System;

namespace Geranium.Modules.Functions
{
    /// <summary>
    /// Function for getting configuration object by cfg type
    /// </summary>
    /// <param name="configType">Configuration type</param>
    /// <returns></returns>
    public delegate object ConfigurationProviderFunc(Type moduleType, Type configType);
}