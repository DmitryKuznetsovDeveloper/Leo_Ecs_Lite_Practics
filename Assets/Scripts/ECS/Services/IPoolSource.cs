using UnityEngine;

namespace ECS.Services
{
    public interface IPoolSource
    {
        GameObject GetInstance(Vector3 position, Quaternion rotation);
    }
}