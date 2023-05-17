using Geranium.Modules.Interfaces;

namespace Geranium.Modules.TestModule1
{
    public class Module : BaseModule
    {
        public override void Install()
        {
            this.Register<IServiceBridge,TestServiceBridge>();
        }
    }
}