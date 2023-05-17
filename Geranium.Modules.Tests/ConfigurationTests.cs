using Castle.Windsor;
using Geranium.Modules.CastleWindsor;
using Geranium.Modules.Installing;
using Geranium.Modules.Models;
using Geranium.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Geranium.Modules.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        [TestMethod]
        public void ConfigurationInMemoryTest()
        {
            var container = new WindsorContainer();
            var bridge = new WindsorServiceBridge(container);

            ModuleInstaller.Install(new ModulesInstallSettings(bridge, ProvideConfigurationFromMemory));

            var modules = bridge.ResolveServices<IModule>();

            Assert.AreNotEqual(0, modules.Count());

            foreach (var module in modules)
            {
                Assert.AreEqual(module.Status, InstallStatus.Installed);
            }
        }

        private object ProvideConfigurationFromMemory( Type moduleType, Type configType)
        {
            var instance = configType.New();
            foreach (var prop in configType.GetProperties())
            {
                instance.SetPropValue(prop.Name, prop.PropertyType.Default());
            }

            return instance;
        }

        [TestMethod]
        public void ConfigurationFromFileTest()
        {
            var container = new WindsorContainer();
            var bridge = new WindsorServiceBridge(container);

            ModuleInstaller.Install(new ModulesInstallSettings(bridge, ProvideConfigurationFromConfiguration));

            var modules = bridge.ResolveServices<IModule>();

            Assert.AreNotEqual(0, modules.Count());

            foreach (var module in modules)
            {
                Assert.AreEqual(module.Status, InstallStatus.Installed);
            }
        }

        private static IConfigurationRoot root;
        private static IConfigurationRoot ConfigurationRoot()
        {
            if (root == default)
            {
                root = new ConfigurationBuilder()
                    .AddJsonFile("geranium.modules.tests.config.json")
                    .Build();
            }

            return root;
        }

        private object ProvideConfigurationFromConfiguration(Type moduleType, Type configType)
        {
            var modulesSection = ConfigurationRoot().GetSection("Modules");

            var configSection = modulesSection.GetSection($"{moduleType.Name}.{configType.Name}");
            var configInstance = configSection.Get(configType);
            if (configInstance == default)
            {
                configSection = modulesSection
                    .GetSection(moduleType.Namespace.Replace("Geranium.", ""));
                configInstance = configSection.Get(configType);

                if (configInstance == default)
                    configInstance = configType.New();
            }

            return configInstance;
        }
    }
}