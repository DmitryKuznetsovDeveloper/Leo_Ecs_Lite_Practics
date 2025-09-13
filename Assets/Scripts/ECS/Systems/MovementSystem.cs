﻿using ECS.Components;
using ECS.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public sealed class MovementSystem : IEcsPostRunSystem
    {
       private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>> _filter;
       
        public void PostRun(IEcsSystems systems)
        {
            var moveDirPool = _filter.Pools.Inc1;
            var moveSpeedPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;
         
            var dt = Time.deltaTime;
            foreach (var entity in _filter.Value)
            {
                var direction = moveDirPool.Get(entity);
                var speed = moveSpeedPool.Get(entity);
                ref var position = ref positionPool.Get(entity);

                position.Value += direction.Value * (speed.Value * dt);
            }
        }
    }
}