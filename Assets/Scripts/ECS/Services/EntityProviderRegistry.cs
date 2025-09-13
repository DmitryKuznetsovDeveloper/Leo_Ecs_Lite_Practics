using System.Collections.Generic;
using Leopotam.EcsLite;

namespace ECS.Services
{
    public sealed class EntityProviderRegistry
    {
        private readonly List<EntityProvider> _providers = new();

        public void RegisterInstaller(EntityProvider provider)
        {
            _providers.Add(provider);
        }

        public void Initialize(EcsWorld world)
        {
            foreach (var installer in _providers)
            {
                installer.InstallToWorld(world);
            }

            _providers.Clear();
        }
        
        public void Dispose()
        {
            _providers.Clear();
        }
    }
}