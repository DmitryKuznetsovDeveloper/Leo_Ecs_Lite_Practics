using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public class BulletProvider : EntityProvider, IPoolableProvider
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _prewarm = 5;
        
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
            entity.AddComponent(new DespawnAfterTime { RemainingTime = 3f });
        }
    }
}