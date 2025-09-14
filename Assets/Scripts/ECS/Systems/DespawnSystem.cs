using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public sealed class DespawnSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<DespawnRequest, TransformView>> _filter;
    private readonly EcsWorldInject _worldGame;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter.Value)
        {
            ref var view = ref _filter.Pools.Inc2.Get(entity);

            if (view.Provider is IPoolableProvider poolable)
            {
                poolable.ReturnToPool();
            }
            else if (view.Value != null)
            {
                Object.Destroy(view.Value.gameObject);
            }

            _worldGame.Value.DelEntity(entity);
        }
    }
}