using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public sealed class WarriorProvider : EntityProvider
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletProvider _bulletPrefab;
        [SerializeField] private float _fireCooldown;
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _health = 100f;
        [SerializeField] [Range(0, 1)] private int _teamId;
        [SerializeField] private float _visionRadius = 8f;
        [SerializeField] private LayerMask _visionMask = ~0;

        public override void Install(Entity entity)
        {
            entity.AddOrReplaceComponent(new Position { Value = transform.position });
            entity.AddOrReplaceComponent(new Rotation { Value = transform.rotation });
            entity.AddOrReplaceComponent(new MoveDirection { Value = transform.forward });
            entity.AddOrReplaceComponent(new MoveSpeed { Value = _moveSpeed });
            entity.AddOrReplaceComponent(new Health { Value = _health });

            entity.AddComponent(new TransformView
            {
                Value = transform,
                Provider = this
            });
            
            entity.AddOrReplaceComponent(new BulletWeapon
            {
                FirePoint = _firePoint,
                BulletPrefab = _bulletPrefab,
            });
            entity.AddOrReplaceComponent(new Team { Value = _teamId });
            
            entity.AddOrReplaceComponent(new Vision {
                Radius = _visionRadius,
                LayerMask = _visionMask
            });
            
            entity.AddOrReplaceComponent(new FireCooldown {
                Value = _fireCooldown,
                TimeLeft = 0f
            });
            
        }
        
        public void SetTeam(int teamId) => _teamId = teamId;
    }
}