using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public sealed class CharacterProvider : EntityProvider
    {
        [SerializeField] private float _moveSpeed = 5f;
        protected override void Install(Entity entity)
        {
            entity.AddComponent(new Position() { Value = transform.position });
            entity.AddComponent(new Rotation() { Value = transform.rotation });
            entity.AddComponent(new MoveDirection() { Value = transform.forward });
            entity.AddComponent(new MoveSpeed() { Value = _moveSpeed });
            entity.AddComponent(new TransformView() { Value = transform });
        }
    }
}