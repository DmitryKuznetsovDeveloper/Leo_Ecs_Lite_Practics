using System;
using ECS.Services;

namespace ECS.Components
{
    [OneFrame]
    [Serializable]
    public struct DeathRequest
    {
        public int Entity;
    }
}