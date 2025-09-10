using Leopotam.EcsLite;
using UnityEngine;
using Zenject;

namespace ECS.Services
{
    public abstract class EntityProvider : MonoBehaviour
    {
        private EntityProviderRegistry _registry;

        [Inject]
        public void Construct(EntityProviderRegistry registry)
        {
            _registry = registry;
            _registry.RegisterInstaller(this);
        }

        public void InstallToWorld(EcsWorld world)
        {
            int id = world.NewEntity();
            var entity = new Entity(world, id);
            Install(entity);
        }

        protected abstract void Install(Entity entity);
    }
}