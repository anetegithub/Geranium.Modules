using Geranium.Modules.Installing;
using Geranium.Modules.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Geranium.Modules
{
    /// <summary>
    /// BaseModule interfaces
    /// </summary>
    public interface IModule : IToposort
    {
        /// <summary>
        /// Is needed for the system to function
        /// </summary>
        bool SystemRequired { get; }

        /// <summary>
        /// Link for module assembly
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Install method
        /// </summary>
        void Install();

        /// <summary>
        /// Bridge of container
        /// </summary>
        IServiceBridge Services { get; set; }

        /// <summary>
        /// Is root dependency for other modules, may be only one
        /// </summary>
        bool IsRoot { get; }

        InstallStatus Status { get; set; }
    }
}