using System;
using ECS.Services;

namespace ECS.Components
{
    [OneFrame]
    [Serializable]
    public struct CollisionRequest
    {
        public int Source;
        public int Target;
    }
}