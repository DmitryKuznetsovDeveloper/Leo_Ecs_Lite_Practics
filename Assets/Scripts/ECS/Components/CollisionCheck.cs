using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct CollisionCheck
    {
        public Vector3 Position;
        public float Radius;
        public LayerMask LayerMask;
    }
}