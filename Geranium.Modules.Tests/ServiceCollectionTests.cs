using Geranium.Modules.Implementations;
using Geranium.Modules.Installing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Geranium.Modules.Tests
{
    [TestClass]
    public class ServiceCollectionTests
    {
        [TestMethod]
        public void SettingsTest()
        {
            var collection = new ServiceCollection();
            collection.AddSingleton(typeof(IServiceCollection), collection);

            var provider = collection.BuildServiceProvider();

            var bridge = new ServiceCollectionBridge(provider);

            var infoes = ModuleInstaller.Install(bridge);

            var modules = bridge.ResolveServices<IModule>();

            Assert.AreNotEqual(0, modules.Count());

            foreach (var info in infoes)
            {
                Assert.AreEqual(info.Module.Status, InstallStatus.Installed);
            }
        }
    }
}
