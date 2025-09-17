using System;
using ECS.Services;
using UnityEngine;

namespace ECS.Components
{
    [OneFrame]
    [Serializable]
    public struct SpawnRequest
    {
        public EntityProvider Prefab;
        public Vector3 Position;
        public Quaternion Rotation;
        public int OwnerEntity; 
    }
}