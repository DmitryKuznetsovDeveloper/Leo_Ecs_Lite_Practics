using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class DamageOnCollisionRequestSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<CollisionRequest>> _filter = WorldNames.EVENTS;
    private readonly EcsWorldInject _worldEvents = WorldNames.EVENTS;
    private readonly EcsWorldInject _worldGame;

    public void Run(IEcsSystems systems)
    {
        var damagePool = _worldGame.Value.GetPool<Damage>();
        var despawnPool = _worldGame.Value.GetPool<DespawnRequest>();

        foreach (var evt in _filter.Value)
        {
            ref var collision = ref _filter.Pools.Inc1.Get(evt);
            
            // наносим урон только если есть Damage
            if (!damagePool.Has(collision.Source))
            {
                continue;
            }

            // если уже стоит DespawnRequest — значит, обработали
            if (despawnPool.Has(collision.Source))
            {
                continue;
            }

            var damageValue = damagePool.Get(collision.Source).Value;

            // создаём DamageRequest
            int dmgEvt = _worldEvents.Value.NewEntity();
            ref var dmg = ref _worldEvents.Value.GetPool<DamageRequest>().Add(dmgEvt);
            dmg.Source = collision.Source;
            dmg.Target = collision.Target;
            dmg.Value = damageValue;

            // сразу помечаем пулю на деспавн
            despawnPool.Add(collision.Source);
        }
    }
}