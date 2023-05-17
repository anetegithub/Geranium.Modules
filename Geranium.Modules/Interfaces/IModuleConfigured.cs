using System;

namespace Geranium.Modules.Interfaces
{
    /// <summary>
    /// Interface of module with configuration
    /// </summary>
    public interface IModuleConfigured : IModule
    {
        /// <summary>
        /// Set configuration instance
        /// </summary>
        /// <param name="configuration"></param>
        void SetConfiguration(object configuration);

        /// <summary>
        /// Get module configuration type
        /// </summary>
        /// <returns></returns>
        Type GetConfigurationType();
    }
}