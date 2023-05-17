using Castle.Windsor;
using Geranium.Modules.CastleWindsor;
using Geranium.Modules.Installing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Geranium.Modules.Tests
{
    [TestClass]
    public class CastleWindsorTests
    {
        [TestMethod]
        public void WindsorTest()
        {
            var container = new WindsorContainer();
            var bridge = new WindsorServiceBridge(container);

            ModuleInstaller.Install(bridge);

            var modules = bridge.ResolveServices<IModule>();

            Assert.AreNotEqual(0, modules.Count());

            foreach (var module in modules)
            {
                Assert.AreEqual(module.Status, InstallStatus.Installed);
            }
        }
    }
}