using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public sealed class BulletProvider : EntityProvider, IPoolableProvider
    {
        [SerializeField] private float _moveSpeed;
        
        private IGameObjectPool _pool;

        public void SetupPool(IGameObjectPool pool)
        {
            _pool = pool;
        }

        public void ReturnToPool()
        {
            _pool?.Return(this);
        }
        public override void Install(Entity entity)
        {
            entity.AddOrReplaceComponent(new Position { Value = transform.position });
            entity.AddOrReplaceComponent(new Rotation { Value = transform.rotation });
            entity.AddOrReplaceComponent(new MoveDirection { Value = transform.forward });
            entity.AddOrReplaceComponent(new MoveSpeed { Value = _moveSpeed });
            entity.AddOrReplaceComponent(new TransformView
            {
                Value = transform,
                Provider = this
            });
            entity.AddOrReplaceComponent(new DespawnAfterTime { RemainingTime = 20f });
            entity.AddOrReplaceComponent(new Damage { Value = 10 });
            entity.AddOrReplaceComponent(new CollisionCheck
            {
                Position = transform.position,
                Radius = 0.25f,
                LayerMask = LayerMask.GetMask("Default")
            });
        }
    }
}