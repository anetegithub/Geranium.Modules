using Geranium.Modules.Interfaces;
using Geranium.Reflection;
using System;

namespace Geranium.Modules
{
    /// <summary>
    /// Base module class with configuration
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseModule<T> : BaseModule, IModuleConfigured<T>
    {
        /// <summary>
        /// Configuration instance
        /// </summary>
        public T Configuration { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public Type GetConfigurationType() => typeof(T);

        /// <summary>
        /// Setting value to <see cref="Configuration"/>, and additionaly setting value for <see cref="IModuleConfigured{T}.StaticConfiguration"/>
        /// </summary>
        /// <param name="configuration"></param>
        public void SetConfiguration(object configuration)
        {
            IModuleConfigured<T>.StaticConfiguration = Configuration = configuration.As<T>();
        }
    }
}