using System;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct Rotation
    {
        public Quaternion Value;
    }
}