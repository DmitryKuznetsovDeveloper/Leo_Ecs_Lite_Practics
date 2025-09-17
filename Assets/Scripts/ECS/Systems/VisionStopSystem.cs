using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class VisionStopSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _worldGame;
        private readonly EcsFilterInject<Inc<Vision, TransformView, Team>> _units;
        private readonly EcsPoolInject<StopMovementTag> _stop;
        private readonly EcsPoolInject<EnemyInSightTag> _sight;

        private readonly Collider[] _hits = new Collider[32];

        public void Run(IEcsSystems systems)
        {
            var teamPool = _worldGame.Value.GetPool<Team>();

            foreach (var e in _units.Value)
            {
                ref var view   = ref _units.Pools.Inc2.Get(e);
                ref var vision = ref _units.Pools.Inc1.Get(e);
                int myTeam     = _units.Pools.Inc3.Get(e).Value;

                var center = view.Value.position;
                int count = Physics.OverlapSphereNonAlloc(center, vision.Radius, _hits, vision.LayerMask);

                bool enemyFound = false;
                for (int i = 0; i < count; i++)
                {
                    var col = _hits[i];
                    if (!col)
                    {
                        continue;
                    }

                    var otherProv = col.GetComponentInParent<EntityProvider>();
                    if (otherProv == null || otherProv == view.Provider)
                    {
                        continue;
                    }

                    int other = otherProv.EntityId;
                    if (other < 0)
                    {
                        continue;
                    }

                    if (teamPool.Has(other) && teamPool.Get(other).Value != myTeam)
                    {
                        enemyFound = true;
                        break;
                    }
                }
                
                if (enemyFound)
                {
                    if (!_stop.Value.Has(e))  _stop.Value.Add(e);
                    if (!_sight.Value.Has(e)) _sight.Value.Add(e);
                }
                else
                {
                    if (_stop.Value.Has(e))  _stop.Value.Del(e);
                    if (_sight.Value.Has(e)) _sight.Value.Del(e);
                }
            }
        }
    }
}
