using System;

namespace ECS.Components
{
    [Serializable]
    public struct DespawnAfterTime
    {
        public float RemainingTime;
    }
}