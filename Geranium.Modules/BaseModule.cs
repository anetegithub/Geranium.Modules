using Geranium.Modules.Interfaces;
using System.Reflection;
using System;
using Microsoft.Extensions.DependencyInjection;
using Geranium.Modules.Installing;

namespace Geranium.Modules
{
    /// <summary>
    /// Base module class
    /// </summary>
    public abstract partial class BaseModule : ToposortType, IModule
    {
        /// <summary>
        /// Default is <see cref="false"/>
        /// </summary>
        public virtual bool IsRoot => false;

        /// <summary>
        /// Default is <see cref="false"/>
        /// </summary>
        public virtual bool SystemRequired => false;

        /// <summary>
        /// Default is <see cref="Object.GetType()"/>.<see cref="Type.Assembly"/>
        /// </summary>
        public virtual Assembly Assembly => GetType().Assembly;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public IServiceBridge Services { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public InstallStatus Status { get; set; } = InstallStatus.Loaded;

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public virtual void Install() { }
    }
}