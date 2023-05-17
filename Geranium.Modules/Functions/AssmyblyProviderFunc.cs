using System.Reflection;
using System.Runtime.Loader;

namespace Geranium.Modules.Functions
{
    /// <summary>
    /// Function getting assemblies with module types
    /// </summary>
    /// <param name="loadContext"></param>
    /// <returns></returns>
    public delegate Assembly[] AssmyblyProviderFunc(AssemblyLoadContext loadContext);
}