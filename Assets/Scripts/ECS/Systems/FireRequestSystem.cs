using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public class FireRequestSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<BulletWeapon>> _weapons;
    private readonly EcsPoolInject<FireRequest> _fireRequestPool;

    private readonly EcsWorldInject _eventWorld = WorldNames.EVENTS;
    private readonly EcsPoolInject<SpawnRequest> _spawnPool = WorldNames.EVENTS;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _weapons.Value)
        {
            if (!_fireRequestPool.Value.Has(entity))
            {
                continue;
            }

            var weapon = _weapons.Pools.Inc1.Get(entity);

            int spawnEntity = _eventWorld.Value.NewEntity();
            _spawnPool.Value.Add(spawnEntity) = new SpawnRequest
            {
                Prefab = weapon.BulletPrefab,
                Position = weapon.FirePoint.position,
                Rotation = weapon.FirePoint.rotation,
            };

#if UNITY_EDITOR
            UnityEngine.Debug.Log($"[FireRequestSystem] SpawnRequest created from FireRequest on entity {entity}");
#endif
        }
    }
}