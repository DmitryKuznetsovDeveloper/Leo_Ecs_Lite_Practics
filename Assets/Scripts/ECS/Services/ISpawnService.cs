using ECS.Components;
using Leopotam.EcsLite;

namespace ECS.Services
{
    public interface ISpawnService
    {
        Entity Spawn(SpawnRequest request, EcsWorld world);
    }
}