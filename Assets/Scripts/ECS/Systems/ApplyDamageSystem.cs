using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public sealed class ApplyDamageSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _worldGame;
        private readonly EcsWorldInject _worldEvents = WorldNames.EVENTS;
        private readonly EcsFilterInject<Inc<DamageRequest>> _filter = WorldNames.EVENTS;

        public void Run(IEcsSystems systems)
        {
            var healthPool = _worldGame.Value.GetPool<Health>();

            foreach (var evt in _filter.Value)
            {
                ref var request = ref _filter.Pools.Inc1.Get(evt);

                if (!healthPool.Has(request.Target))
                {
                    continue;
                }
                
                ref var health = ref healthPool.Get(request.Target);
                health.Value -= request.Value;
                    
                if (health.Value <= 0)
                {
                    int deathEvt = _worldEvents.Value.NewEntity();
                    ref var death = ref _worldEvents.Value.GetPool<DeathRequest>().Add(deathEvt);
                    death.Entity = request.Target;
                }
            }
        }
    }
}