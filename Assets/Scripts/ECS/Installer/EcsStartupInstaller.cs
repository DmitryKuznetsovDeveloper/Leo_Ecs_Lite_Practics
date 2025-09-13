using ECS.Services;
using Zenject;

namespace ECS.Installer
{
    public sealed class EcsStartupInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EntityProviderRegistry>().AsSingle();
            Container.BindInterfacesTo<EcsStartup>().AsSingle().NonLazy();
        }
    }
}