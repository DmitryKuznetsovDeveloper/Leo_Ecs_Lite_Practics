using System;
using ECS.Services;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
        public EntityProvider Provider;
    }
}