using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class SpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _gameWorld;
        private readonly EcsFilterInject<Inc<SpawnRequest>> _spawnRequests = WorldNames.EVENTS;
        private readonly EcsPoolInject<SpawnRequest> _spawnPool = WorldNames.EVENTS;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _spawnRequests.Value)
            {
                var request = _spawnPool.Value.Get(entity);

                if (request.Prefab == null)
                    continue;

                var instanceGO = Object.Instantiate(request.Prefab.gameObject, request.Position, request.Rotation);
                var provider = instanceGO.GetComponent<EntityProvider>();

                var ecsEntity = new Entity(_gameWorld.Value, _gameWorld.Value.NewEntity());
                provider.Install(ecsEntity);
            }
        }
    }
}