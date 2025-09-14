using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public sealed class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>> _filter = WorldNames.EVENTS;
        private readonly EcsWorldInject _worldGame;

        public void Run(IEcsSystems systems)
        {
            var despawnPool = _worldGame.Value.GetPool<DespawnRequest>();

            foreach (var evt in _filter.Value)
            {
                ref var death = ref _filter.Pools.Inc1.Get(evt);
                
                if (!despawnPool.Has(death.Entity))
                {
                    despawnPool.Add(death.Entity);
                }
            }
        }
    }
}