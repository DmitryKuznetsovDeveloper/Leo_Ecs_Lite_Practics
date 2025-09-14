using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public sealed class CharacterProvider : EntityProvider
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletProvider _bulletPrefab;
        [SerializeField] private float _moveSpeed = 5f;

        public override void Install(Entity entity)
        {
            entity.AddComponent(new PlayerTag());
            entity.AddComponent(new Position { Value = transform.position });
            entity.AddComponent(new Rotation { Value = transform.rotation });
            entity.AddComponent(new MoveDirection { Value = transform.forward });
            entity.AddComponent(new MoveSpeed { Value = _moveSpeed });
            entity.AddOrReplaceComponent(new Health { Value = 100f });
            entity.AddComponent(new TransformView
            {
                Value = transform,
                Provider = this
            });
            entity.AddComponent(new BulletWeapon
            {
                FirePoint = _firePoint,
                BulletPrefab = _bulletPrefab,
            });
        }
    }
}