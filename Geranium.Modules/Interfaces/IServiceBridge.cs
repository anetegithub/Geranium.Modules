using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Geranium.Modules.Interfaces
{
    /// <summary>
    /// IoC container bridge
    /// </summary>
    public interface IServiceBridge
    {
        T ResolveService<T>();

        IEnumerable<T> ResolveServices<T>();

        object ResolveService(Type type);

        IEnumerable<object> ResolveServices(Type type);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <typeparam name="TFor"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="isDefault"></param>
        void RegisterTransient<TFor, TImplementation>(bool isDefault = false);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="for"></param>
        void RegisterTransient(Type impl, params Type[] @for);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="isDefault"></param>
        /// <param name="for"></param>
        void RegisterTransient(Type impl, bool isDefault = false, params Type[] @for);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <typeparam name="TFor"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="isDefault"></param>
        void RegisterSingleton<TFor, TImplementation>(bool isDefault = false);

        /// <summary>
        /// Register component instance in container
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="for"></param>
        void RegisterSingleton(Type impl, object instance);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="impl"></param>
        /// <param name="for"></param>
        void RegisterSingleton(Type impl, params Type[] @for);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="isDefault"></param>
        /// <param name="for"></param>
        void RegisterSingleton(Type impl, bool isDefault = false, params Type[] @for);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <typeparam name="TFor"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="isDefault"></param>
        void RegisterScoped<TFor, TImplementation>(bool isDefault = false);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="for"></param>
        void RegisterScoped(Type impl, params Type[] @for);

        /// <summary>
        /// Register component in container
        /// </summary>
        /// <param name="comp"></param>
        /// <param name="isDefault"></param>
        /// <param name="for"></param>
        void RegisterScoped(Type impl, bool isDefault = false, params Type[] @for);
    }
}