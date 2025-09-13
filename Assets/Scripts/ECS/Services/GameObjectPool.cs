using System.Collections.Generic;
using UnityEngine;

namespace ECS.Services
{
    public sealed class GameObjectPool<T> : IGameObjectPool where T : EntityProvider
    {
        private readonly T _prefab;
        private readonly Stack<T> _pool = new();

        public GameObjectPool(T prefab, int prewarm = 0)
        {
            _prefab = prefab;

            for (int i = 0; i < prewarm; i++)
            {
                var go = Object.Instantiate(_prefab.gameObject);
                go.SetActive(false);
                _pool.Push(go.GetComponent<T>());
            }
        }

        public T Get(Vector3 pos, Quaternion rot)
        {
            T instance = _pool.Count > 0 ? _pool.Pop() : Object.Instantiate(_prefab);
            var go = instance.gameObject;

            go.SetActive(true);
            go.transform.SetPositionAndRotation(pos, rot);
            return instance;
        }

        public void Return(EntityProvider provider)
        {
            if (provider is T typed)
            {
                typed.gameObject.SetActive(false);
                _pool.Push(typed);
            }
        }
    }
}