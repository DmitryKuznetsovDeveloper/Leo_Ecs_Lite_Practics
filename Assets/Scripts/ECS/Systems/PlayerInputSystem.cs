using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag, MoveDirection>> _filter;
        private readonly EcsPoolInject<MoveDirection> _moveDirPool;

        public void Run(IEcsSystems systems)
        {
            Vector3 input = new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical")
            ).normalized;

            foreach (var entity in _filter.Value)
            {
                ref var dir = ref _moveDirPool.Value.Get(entity);
                dir.Value = input;
            }
        }
    }
}