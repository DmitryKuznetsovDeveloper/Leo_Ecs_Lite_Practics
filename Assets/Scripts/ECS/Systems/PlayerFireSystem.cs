using ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerFireSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerTag>> _players;
        private readonly EcsPoolInject<FireRequest> _fireRequests;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _players.Value)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    _fireRequests.Value.Add(playerEntity);
                }
            }
        }
    }
}