using Geranium.Reflection;
using System;

namespace Geranium.Modules.DefaultFunctions
{
    /// <summary>
    /// Holder of default extract modules function
    /// </summary>
    public static class DefaultConfigFunc
    {
        /// <summary>
        /// Getting module info from assemblies
        /// </summary>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static object GetConfig(Type moduleType, Type configType) => configType.New();
    }
}