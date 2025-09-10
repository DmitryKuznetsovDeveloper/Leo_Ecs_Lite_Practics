using System.Collections.Generic;
using ECS.Views;
using Leopotam.EcsLite;

namespace ECS.Services
{
    public sealed class EntityProviderRegistry
    {
        private readonly List<EntityProvider> _installers = new();

        public void RegisterInstaller(EntityProvider provider)
        {
            _installers.Add(provider);
        }

        public void Initialize(EcsWorld world)
        {
            foreach (var installer in _installers)
            {
                installer.InstallToWorld(world);
            }

            _installers.Clear();
        }
    }
}