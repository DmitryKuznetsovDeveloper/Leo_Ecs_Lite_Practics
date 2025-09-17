using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace ECS.Services
{
    public abstract class EntityProvider : MonoBehaviour
    {
        public int EntityId { get; private set; }
        private EntityProviderRegistry _registry;

        [Inject]
        public void Construct(EntityProviderRegistry registry)
        {
            _registry = registry;
            _registry.RegisterInstaller(this);
        }
        
        public void BindEntityId(int id) => EntityId = id;

        public abstract void Install(Entity entity);

        public void InstallToWorld(EcsWorld world)
        {
            int id = world.NewEntity();
            EntityId = id;
            var entity = new Entity(world, id);
            Install(entity);
        }
    }
}