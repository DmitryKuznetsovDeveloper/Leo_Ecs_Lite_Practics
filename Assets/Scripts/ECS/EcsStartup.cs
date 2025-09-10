using System;
using Client;
using ECS.Services;
using ECS.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace ECS
{
    public sealed class EcsStartup : IInitializable, ITickable, IDisposable
    {
        private readonly EntityProviderRegistry _registry;
        private EcsWorld _world;
        private IEcsSystems _systems;

        public EcsStartup(EntityProviderRegistry registry)
        {
            _registry = registry;
        }

        public void Initialize()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world)
                .Add(new MovementSystem())
                
                
                
                
                
                .Add(new TransformViewSystem());

#if UNITY_EDITOR
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _registry.Initialize(_world);
            _systems.Inject().Init();
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            _systems?.Destroy();
            _systems = null;

            _world?.Destroy();
            _world = null;
        }
    }
}