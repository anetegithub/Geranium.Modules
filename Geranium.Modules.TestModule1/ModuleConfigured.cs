using Geranium.Modules.Interfaces;

namespace Geranium.Modules.TestModule1
{
    public class ModuleConfigured : BaseModule<ModuleConfig>
    {
        protected override void SetDependencies()
        {
            this.DependsOn<Module>();
        }
    }
}
