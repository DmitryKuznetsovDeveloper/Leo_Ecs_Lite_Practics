using System.Collections.Generic;
using ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Services
{
    public class SpawnService : ISpawnService
    {
        private readonly Dictionary<EntityProvider, IGameObjectPool> _pools = new();

        public Entity Spawn(SpawnRequest request, EcsWorld world)
        {
            EntityProvider instance;

            if (request.Prefab is IPoolableProvider)
            {
                if (!_pools.TryGetValue(request.Prefab, out var pool))
                {
                    pool = new GameObjectPool<EntityProvider>(request.Prefab, prewarm: 8);
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

            int id = world.NewEntity();
            var entity = new Entity(world, id);
            instance.Install(entity);
            return entity;
        }
    }
}