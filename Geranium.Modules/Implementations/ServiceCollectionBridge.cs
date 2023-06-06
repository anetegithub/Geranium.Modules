using Geranium.Modules.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Geranium.Modules.Implementations
{
    /// <summary>
    /// In memory ioc container bridge
    /// </summary>
    public class ServiceCollectionBridge : IServiceBridge
    {
        IServiceProvider provider;
        IServiceCollection collection;

        IServiceProvider serviceProvider => serviceCollection?.BuildServiceProvider();
        IServiceCollection serviceCollection => collection ?? provider?.GetService<IServiceCollection>();

        public ServiceCollectionBridge(IServiceProvider provider = default)
        {
            if (provider == null)
            {
                collection = new ServiceCollection();
            }
            else
            {
                this.provider = provider;
            }
        }

        /// <inheritdoc/>
        public void RegisterScoped<TFor, TImplementation>(bool isDefault = false)
            => RegisterScoped(typeof(TImplementation), isDefault, typeof(TFor));

        /// <inheritdoc/>
        public void RegisterScoped(Type impl, params Type[] @for)
            => RegisterScoped(impl, false, @for);

        /// <inheritdoc/>
        public void RegisterScoped(Type impl, bool isDefault = false, params Type[] @for)
            => AddAs(impl, @for, serviceCollection.AddScoped);

        /// <inheritdoc/>
        public void RegisterSingleton(Type impl, object instance)
            => collection.AddSingleton(impl, instance);

        /// <inheritdoc/>
        public void RegisterSingleton<TFor, TImplementation>(bool isDefault = false)
            => RegisterSingleton(typeof(TImplementation), isDefault, typeof(TFor));

        /// <inheritdoc/>
        public void RegisterSingleton(Type impl, params Type[] @for)
            => RegisterSingleton(impl, false, @for);

        /// <inheritdoc/>
        public void RegisterSingleton(Type impl, bool isDefault = false, params Type[] @for)
            => AddAs(impl, @for, serviceCollection.AddSingleton);

        /// <inheritdoc/>
        public void RegisterTransient<TFor, TImplementation>(bool isDefault = false)
            => RegisterTransient(typeof(TImplementation), isDefault, typeof(TFor));

        /// <inheritdoc/>
        public void RegisterTransient(Type impl, params Type[] @for)
            => RegisterTransient(impl, false, @for);

        /// <inheritdoc/>
        public void RegisterTransient(Type impl, bool isDefault = false, params Type[] @for)
            => AddAs(impl, @for, serviceCollection.AddTransient);

        private void AddAs(Type impl, Type[] @for, Func<Type, Type, IServiceCollection> addFunc)
        {
            for (int i = 0; i < @for.Length; i++)
            {
                addFunc(@for[i], impl);
            }
        }

        /// <inheritdoc/>
        public T ResolveService<T>() => serviceProvider.GetService<T>();

        /// <inheritdoc/>
        public object ResolveService(Type type) => serviceProvider.GetService(type);

        public IEnumerable<T> ResolveServices<T>() => serviceProvider.GetServices<T>();

        public IEnumerable<object> ResolveServices(Type type) => serviceProvider.GetServices(type);

        public bool Release(object instance) => true;
    }
}