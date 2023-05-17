using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Geranium.Modules.DefaultFunctions
{
    /// <summary>
    /// Holder of function getting assemblies from files 
    /// </summary>
    public static class AssemblyFromFiles
    {
        /// <summary>
        /// Get assemblies from <see cref="AppContext.BaseDirectory"/> and loaded by <see cref="AssemblyLoadContext"/>
        /// </summary>
        /// <param name="loadContext"></param>
        /// <returns></returns>
        public static Assembly[] GetFiles(AssemblyLoadContext loadContext)
        {
            var moduleAsms = new List<Assembly>();

            var asmPaths = Directory.GetFiles(AppContext.BaseDirectory, "*.dll");
            foreach (var asmPath in asmPaths)
            {
                var asmDef = AssemblyDefinition.ReadAssembly(asmPath);
                var isModule = asmDef.MainModule.Types
                    .FirstOrDefault(x =>
                    {
                        var baseModuleFullName = typeof(BaseModule).FullName;
                        var baseModuleGenFullName = typeof(BaseModule<>).FullName;

                        if (x.FullName==baseModuleFullName || x.FullName == baseModuleGenFullName)
                            return false;

                        return x?.BaseType?.FullName?.Contains(baseModuleFullName) ?? false;
                    }) != default;

                if (isModule)
                {
                    try
                    {
                        var assembly = loadContext.LoadFromAssemblyPath(asmPath);
                        moduleAsms.Add(assembly);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return moduleAsms.ToArray();
        }
    }
}