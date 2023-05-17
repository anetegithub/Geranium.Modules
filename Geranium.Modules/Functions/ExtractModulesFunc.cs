using Geranium.Modules.Models;
using System.Reflection;

namespace Geranium.Modules.Functions
{
    /// <summary>
    /// Function extracting module infoes from assemblies
    /// </summary>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public delegate ModuleInfo[] ExtractModulesFunc(Assembly[] assemblies);
}