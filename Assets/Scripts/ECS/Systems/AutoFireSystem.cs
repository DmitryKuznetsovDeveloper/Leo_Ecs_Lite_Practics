using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class AutoFireSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletWeapon, FireCooldown, EnemyInSightTag>> _filter;
        private readonly EcsPoolInject<FireRequest> _fireReq;

        public void Run(IEcsSystems systems)
        {
            float dt = Time.deltaTime;

            foreach (var entyti in _filter.Value)
            {
                ref var cd = ref _filter.Pools.Inc2.Get(entyti);
                cd.TimeLeft -= dt;

                if (cd.TimeLeft <= 0f)
                {
                    if (!_fireReq.Value.Has(entyti))
                        _fireReq.Value.Add(entyti);

                    cd.TimeLeft = Mathf.Max(0.01f, cd.Value);
                }
            }
        }
    }
}