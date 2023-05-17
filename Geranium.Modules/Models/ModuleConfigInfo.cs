using System;

namespace Geranium.Modules.Models
{
    /// <summary>
    /// Information about module configuration
    /// </summary>
    public class ModuleConfigInfo
    {
        /// <summary>
        /// Type of configuration
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Instance of configuration
        /// </summary>
        public object Instance { get; set; }
    }
}
