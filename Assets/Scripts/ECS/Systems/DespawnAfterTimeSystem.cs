using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class DespawnAfterTimeSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<DespawnAfterTime>> _filter;
    private readonly EcsPoolInject<DespawnRequest> _despawn;

    public void Run(IEcsSystems system)
    {
        float dt = UnityEngine.Time.deltaTime;
        foreach (var entity in _filter.Value)
        {
            ref var t = ref _filter.Pools.Inc1.Get(entity);
            t.RemainingTime -= dt;
            if (t.RemainingTime <= 0f && !_despawn.Value.Has(entity))
            {
                _despawn.Value.Add(entity);
            }
        }
    }
}