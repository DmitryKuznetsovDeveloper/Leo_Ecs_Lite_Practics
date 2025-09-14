using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _gameWorld;
        private readonly EcsFilterInject<Inc<SpawnRequest>> _spawnRequests = WorldNames.EVENTS;
        private readonly SpawnService _spawnService;

        public SpawnRequestSystem(SpawnService spawnService)
        {
            _spawnService = spawnService;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _spawnRequests.Value)
            {
                var request = _spawnRequests.Pools.Inc1.Get(entity);

                if (request.Prefab == null)
                {
                    continue;
                }

                _spawnService.Spawn(request, _gameWorld.Value);
            }
        }
    }
}