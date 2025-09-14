

using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class CollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionCheck, TransformView>> _filter;
        private readonly EcsWorldInject _worldEvents = WorldNames.EVENTS;

        private readonly Collider[] _overlapResults = new Collider[16];

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var check = ref _filter.Pools.Inc1.Get(entity);
                ref var view = ref _filter.Pools.Inc2.Get(entity);

                check.Position = view.Value.position;
                
                int hitCount = Physics.OverlapSphereNonAlloc(
                    check.Position,
                    check.Radius,
                    _overlapResults,
                    check.LayerMask
                );

                for (int i = 0; i < hitCount; i++)
                {
                    var collider = _overlapResults[i];
                    if (collider == null)
                    {
                        continue;
                    }
                    
                    var otherProvider = collider.GetComponentInParent<EntityProvider>();
                    if (otherProvider == null || otherProvider == view.Provider) 
                        continue;
                    
                    int evt = _worldEvents.Value.NewEntity();
                    ref var collision = ref _worldEvents.Value.GetPool<CollisionRequest>().Add(evt);
                    collision.Source = entity;
                    collision.Target = otherProvider.EntityId;
                }
            }
        }
    }
}