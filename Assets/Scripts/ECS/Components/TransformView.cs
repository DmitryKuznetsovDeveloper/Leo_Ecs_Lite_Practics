using System;
using ECS.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ECS.Components
{
    [Serializable]
    public struct TransformView
    {
        public Transform Value;
        public EntityProvider Provider;
    }
}