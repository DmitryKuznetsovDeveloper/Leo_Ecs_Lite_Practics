using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class ArmySpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _events = WorldNames.EVENTS;
        private readonly EcsFilterInject<Inc<ArmySpawnConfig>> _cfgFilter;

        public void Init(IEcsSystems systems)
        {
            var spawnPool = _events.Value.GetPool<SpawnRequest>();

            foreach (var e in _cfgFilter.Value)
            {
                ref var cfg = ref _cfgFilter.Pools.Inc1.Get(e);

                var redLook = cfg.RedForward.sqrMagnitude > 0.0001f ? cfg.RedForward : Vector3.right;
                var blueLook = cfg.BlueForward.sqrMagnitude > 0.0001f ? cfg.BlueForward : Vector3.left;

                var redRot = Quaternion.LookRotation(redLook, Vector3.up);
                var blueRot = Quaternion.LookRotation(blueLook, Vector3.up);

                for (int r = 0; r < cfg.Rows; r++)
                {
                    for (int c = 0; c < cfg.Cols; c++)
                    {
                        Vector3 cell = new(c * cfg.Spacing, 0f, r * cfg.Spacing);

                        int redEvt = _events.Value.NewEntity();
                        spawnPool.Add(redEvt) = new SpawnRequest
                        {
                            Prefab = cfg.RedPrefab,
                            Position = cfg.RedOrigin + cell,
                            Rotation = redRot
                        };

                        int blueEvt = _events.Value.NewEntity();
                        spawnPool.Add(blueEvt) = new SpawnRequest
                        {
                            Prefab = cfg.BluePrefab,
                            Position = cfg.BlueOrigin - cell,
                            Rotation = blueRot
                        };
                    }
                }
            }
        }
    }
}