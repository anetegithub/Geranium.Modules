using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Geranium.Modules.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Geranium.Modules.CastleWindsor
{
    public class WindsorServiceBridge : IServiceBridge
    {
        private IWindsorContainer _container;

        public WindsorServiceBridge(IWindsorContainer windsorContainer = default)
        {
            _container=windsorContainer;
            if (_container==null)
                _container = new WindsorContainer();
        }

        private void RegisterBase(Type impl, Type[] @for, bool isDefault, ServiceLifetime lifetime, object instance=null)
        {
            var comp = instance == default
                ? Component.For(@for).ImplementedBy(impl)
                : Component.For(@for).UsingFactoryMethod((k, c) => instance);

            if (isDefault)
                comp=comp.IsDefault();

            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    comp=comp.LifestyleSingleton();
                    break;
                case ServiceLifetime.Scoped:
                    comp=comp.LifestyleScoped();
                    break;
                case ServiceLifetime.Transient:
                    comp=comp.LifestyleTransient();
                    break;
                default:
                    break;
            }

            _container.Register(comp);
        }

        public void RegisterScoped<TFor, TImplementation>(bool isDefault = false)
            => RegisterBase(typeof(TImplementation), new Type[] { typeof(TFor) }, isDefault, ServiceLifetime.Scoped);

        public void RegisterScoped(Type impl, params Type[] @for)
            => RegisterBase(impl, @for, false, ServiceLifetime.Scoped);

        public void RegisterScoped(Type impl, bool isDefault = false, params Type[] @for)
            => RegisterBase(impl, @for, isDefault, ServiceLifetime.Scoped);

        public void RegisterSingleton<TFor, TImplementation>(bool isDefault = false)
            => RegisterBase(typeof(TImplementation), new Type[] { typeof(TFor) }, isDefault, ServiceLifetime.Singleton);

        public void RegisterSingleton(Type impl, object instance)
            => RegisterBase(impl, new Type[] { impl, instance.GetType() }, false, ServiceLifetime.Singleton, instance);

        public void RegisterSingleton(Type impl, params Type[] @for)
            => RegisterBase(impl, @for, false, ServiceLifetime.Singleton);

        public void RegisterSingleton(Type impl, bool isDefault = false, params Type[] @for)
            => RegisterBase(impl, @for, isDefault, ServiceLifetime.Singleton);

        public void RegisterTransient<TFor, TImplementation>(bool isDefault = false)
            => RegisterBase(typeof(TImplementation), new Type[] { typeof(TFor) }, isDefault, ServiceLifetime.Transient);

        public void RegisterTransient(Type impl, params Type[] @for)
            => RegisterBase(impl, @for, false, ServiceLifetime.Transient);

        public void RegisterTransient(Type impl, bool isDefault = false, params Type[] @for)
            => RegisterBase(impl, @for, isDefault, ServiceLifetime.Transient);

        public T ResolveService<T>()
        {
            try
            {
                return _container.Resolve<T>();
            }
            catch (ComponentNotFoundException)
            {
                return default;
            }
        }

        public object ResolveService(Type type)
        {
            try
            {
                return _container.Resolve(type);
            }
            catch (ComponentNotFoundException)
            {
                return default;
            }
        }

        public IEnumerable<T> ResolveServices<T>() => _container.ResolveAll<T>();

        public IEnumerable<object> ResolveServices(Type type)
        {
            List<object> list = new List<object>();

            foreach (var item in _container.ResolveAll(type))
            {
                list.Add(item);
            }

            return list;
        }
    }
}
