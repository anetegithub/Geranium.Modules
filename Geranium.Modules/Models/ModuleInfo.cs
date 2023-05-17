using Geranium.Modules.Interfaces;
using System;
using System.Reflection;

namespace Geranium.Modules.Models
{
    /// <summary>
    /// Information about module
    /// </summary>
    public class ModuleInfo
    {
        /// <summary>
        /// Assembly where module is defined
        /// </summary>
        public Assembly ModuleAssembly { get; set; }

        /// <summary>
        /// Type of module
        /// </summary>
        public Type ModuleType { get; set; }

        /// <summary>
        /// BaseModule instance
        /// </summary>
        public IModule Module { get; set; }

        /// <summary>
        /// Configuration instance of module, if module is <see cref="IModuleConfigured"/>
        /// </summary>
        public ModuleConfigInfo Configuration { get; set; }
    }
}