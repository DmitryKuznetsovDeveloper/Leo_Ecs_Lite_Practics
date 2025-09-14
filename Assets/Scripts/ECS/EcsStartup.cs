using System;
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
        private EcsWorld _worldGame;
        private EcsWorld _worldEvents;
        private IEcsSystems _systems;
        
        private SpawnService _spawnService;

        public EcsStartup(EntityProviderRegistry registry)
        {
            _registry = registry;
        }

        public void Initialize()
        {
            _worldGame = new EcsWorld();
            _worldEvents = new EcsWorld();
            _spawnService = new SpawnService();
            _systems = new EcsSystems(_worldGame, WorldNames.GAME)
                .AddWorld(_worldEvents, WorldNames.EVENTS)
                .Add(new OneFrameCleanupSystemGroup(_worldGame))
                .Add(new OneFrameCleanupSystemGroup(_worldEvents))
                .Add(new PlayerInputSystem())
                .Add(new PlayerFireSystem())
                .Add(new MovementSystem())
                .Add(new FireRequestSystem())
                .Add(new SpawnRequestSystem(_spawnService))
                //Colision
                .Add(new CollisionRequestSystem())
                .Add(new DamageOnCollisionRequestSystem())
                .Add(new ApplyDamageSystem())
                .Add(new DespawnSystem())
                
                //View
                .Add(new TransformViewSystem());

#if UNITY_EDITOR
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(WorldNames.EVENTS));
#endif

            _registry.Initialize(_worldGame);
            _systems.Inject().Init();
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            _registry.Dispose();
                
            _systems?.Destroy();
            _systems = null;

            _worldGame?.Destroy();
            _worldGame = null;
            
            _worldEvents?.Destroy();
            _worldEvents = null;
        }
    }
}