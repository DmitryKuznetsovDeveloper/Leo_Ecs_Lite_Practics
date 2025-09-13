using System.Collections.Generic;
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
        private readonly Dictionary<EntityProvider, IGameObjectPool> _pools = new();

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _spawnRequests.Value)
            {
                ref var request = ref _spawnRequests.Pools.Inc1.Get(entity);
                if (request.Prefab == null)
                    continue;

                EntityProvider instance;
            
                if (request.Prefab is IPoolableProvider)
                {
                    if (!_pools.TryGetValue(request.Prefab, out var pool))
                    {
                        pool = new GameObjectPool<EntityProvider>(request.Prefab, prewarm: 5);
                        _pools.Add(request.Prefab, pool);
                    }

                    instance = ((GameObjectPool<EntityProvider>)pool).Get(request.Position, request.Rotation);
                    ((IPoolableProvider)instance).SetupPool(pool);
                }
                else
                {
                    var go = Object.Instantiate(request.Prefab.gameObject);
                    go.transform.SetPositionAndRotation(request.Position, request.Rotation);
                    instance = go.GetComponent<EntityProvider>();
                }
            
                int id = _gameWorld.Value.NewEntity();
                var spawned = new Entity(_gameWorld.Value, id);
                instance.Install(spawned);
            }
        }
    }
}