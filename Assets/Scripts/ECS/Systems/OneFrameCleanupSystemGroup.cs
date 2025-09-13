using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Services;
using Leopotam.EcsLite;

public class OneFrameCleanupSystemGroup : IEcsRunSystem
{
    private readonly List<IInternalCleanupSystem> _cleanupSystems = new();

    public OneFrameCleanupSystemGroup(EcsWorld world)
    {
        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(x => x.GetTypes())
            .Where(t => t.IsValueType && t.IsDefined(typeof(OneFrameAttribute), false));

        foreach (var type in allTypes)
        {
            var system = (IInternalCleanupSystem)Activator.CreateInstance(
                typeof(InternalCleanupSystem<>).MakeGenericType(type),
                world
            );

            _cleanupSystems.Add(system);
        }
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var system in _cleanupSystems)
            system.Run();
    }

    private interface IInternalCleanupSystem
    {
        void Run();
    }

    private class InternalCleanupSystem<T> : IInternalCleanupSystem where T : struct
    {
        private readonly EcsWorld _world;
        private readonly EcsPool<T> _pool;
        private readonly EcsFilter _filter;

        public InternalCleanupSystem(EcsWorld world)
        {
            _world = world;
            _pool = _world.GetPool<T>();
            _filter = _world.Filter<T>().End();
        }

        public void Run()
        {
            foreach (var entity in _filter)
            {
#if UNITY_EDITOR
                UnityEngine.Debug.Log($"[OneFrameCleanup] Removing <{typeof(T).Name}> from entity {entity}");
#endif
                _pool.Del(entity);
            }
        }
    }
}