using Geranium.Modules.Interfaces;

namespace Geranium.Modules.TestModule1
{
    internal class TestServiceBridge : IServiceBridge
    {
        public void RegisterScoped<TFor, TImplementation>(bool isDefault = false)
        {
            throw new NotImplementedException();
        }

        public void RegisterScoped(Type impl, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public void RegisterScoped(Type impl, bool isDefault = false, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton<TFor, TImplementation>(bool isDefault = false)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type impl, object instance)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type impl, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public void RegisterSingleton(Type impl, bool isDefault = false, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public void RegisterTransient<TFor, TImplementation>(bool isDefault = false)
        {
            throw new NotImplementedException();
        }

        public void RegisterTransient(Type impl, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public void RegisterTransient(Type impl, bool isDefault = false, params Type[] @for)
        {
            throw new NotImplementedException();
        }

        public bool Release(object instance) => true;

        public T ResolveService<T>()
        {
            throw new NotImplementedException();
        }

        public object ResolveService(Type type)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> ResolveServices<T>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveServices(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
