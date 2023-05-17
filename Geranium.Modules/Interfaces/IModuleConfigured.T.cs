namespace Geranium.Modules.Interfaces
{
    public interface IModuleConfigured<T> : IModuleConfigured
    {
        T Configuration { get; set; }

        public static T StaticConfiguration { get; set; }
    }
}
