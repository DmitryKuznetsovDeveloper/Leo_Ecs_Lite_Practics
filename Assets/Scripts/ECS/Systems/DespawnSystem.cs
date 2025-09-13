// ECS/Systems/DespawnSystem.cs
using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class DespawnSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DespawnAfterTime, TransformView>> _filter = default;
        private readonly EcsPoolInject<DespawnAfterTime> _despawnPool = default;
        private readonly EcsPoolInject<TransformView> _viewPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var timer = ref _despawnPool.Value.Get(entity);
                timer.RemainingTime -= Time.deltaTime;

                if (timer.RemainingTime <= 0f)
                {
                    var view = _viewPool.Value.Get(entity).Value;
                    Object.Destroy(view.gameObject);

                    _despawnPool.Value.Del(entity);
                    _viewPool.Value.Del(entity);
                }
            }
        }
    }
}