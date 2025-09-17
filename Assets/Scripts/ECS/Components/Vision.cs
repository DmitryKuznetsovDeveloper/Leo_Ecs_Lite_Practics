using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct Vision
    {
        public float Radius;
        public LayerMask LayerMask;
    }
}