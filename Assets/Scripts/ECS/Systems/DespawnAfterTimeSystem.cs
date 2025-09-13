using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class DespawnAfterTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DespawnAfterTime, TransformView>> _filter;
        private readonly EcsPoolInject<DespawnAfterTime> _despawnPool;
        private readonly EcsPoolInject<TransformView> _viewPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var timer = ref _despawnPool.Value.Get(entity);
                timer.RemainingTime -= Time.deltaTime;

                if (timer.RemainingTime > 0f)
                    continue;

                ref var view = ref _viewPool.Value.Get(entity);

                if (view.Provider is IPoolableProvider poolable)
                {
                    poolable.ReturnToPool();
                }
                else if (view.Value != null)
                {
                    Object.Destroy(view.Value.gameObject);
                }

                _despawnPool.Value.Del(entity);
                _viewPool.Value.Del(entity);
            }
        }
    }
}