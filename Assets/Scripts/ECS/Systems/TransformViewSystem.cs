using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Client
{
    public sealed class TransformViewSystem : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>> _filter = default;
        private readonly EcsPoolInject<Rotation> _rotationPool = default;

        public void PostRun(IEcsSystems systems)
        {
            var rotationPool = _rotationPool.Value;

            foreach (int entity in _filter.Value)
            {
                ref var transform = ref _filter.Pools.Inc1.Get(entity);
                var position = _filter.Pools.Inc2.Get(entity);

                transform.Value.position = position.Value;

                if (!rotationPool.Has(entity))
                {
                    continue;
                }
                
                var rotation = rotationPool.Get(entity).Value;
                transform.Value.rotation = rotation;
            }
        }
    }
}