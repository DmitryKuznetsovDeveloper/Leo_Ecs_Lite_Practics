using Leopotam.EcsLite;

namespace ECS.Services
{
    public readonly struct Entity
    {
        public readonly int Id;
        private readonly EcsWorld _world;

        public Entity(EcsWorld world, int id)
        {
            _world = world;
            Id = id;
        }
        
        public ref T AddComponent<T>(T component) where T : struct
        {
            var pool = _world.GetPool<T>();
            ref var data = ref pool.Add(Id);
            data = component;
            return ref data;
        }
        
        public ref T GetComponent<T>() where T : struct
        {
            return ref _world.GetPool<T>().Get(Id);
        }
        
        public void DelComponent<T>() where T : struct
        {
            var pool = _world.GetPool<T>();
            if (pool.Has(Id))
            {
                pool.Del(Id);
            }
        }
        
        public bool HasComponent<T>() where T : struct
        {
            return _world.GetPool<T>().Has(Id);
        }
    }
}