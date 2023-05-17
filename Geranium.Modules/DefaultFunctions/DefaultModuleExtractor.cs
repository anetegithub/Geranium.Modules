using Geranium.Modules.Models;
using System.Linq;
using System.Reflection;

namespace Geranium.Modules.DefaultFunctions
{
    /// <summary>
    /// Holder of default extract modules function
    /// </summary>
    public static class DefaultModuleExtractor
    {
        /// <summary>
        /// Getting module info from assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static ModuleInfo[] ExtractModules(Assembly[] assemblies)
        {
            var moduleInfoes = assemblies.SelectMany(asm => asm.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t)).Select(m => new ModuleInfo()
            {
                ModuleAssembly = asm,
                ModuleType = m
            })).ToArray();

            return moduleInfoes;
        }
    }
}