using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public class BulletProvider : EntityProvider
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private int _prewarm = 5;
        
        public override void Install(Entity entity)
        {
            entity.AddComponent(new Position { Value = transform.position });
            entity.AddComponent(new Rotation { Value = transform.rotation });
            entity.AddComponent(new MoveDirection { Value = transform.forward });
            entity.AddComponent(new MoveSpeed { Value = _moveSpeed });
            entity.AddComponent(new TransformView { Value = transform });
            entity.AddComponent(new DespawnAfterTime { RemainingTime = 3f });
        }
    }
}