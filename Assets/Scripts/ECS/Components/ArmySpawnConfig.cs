using System;
using ECS.Services;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct ArmySpawnConfig
    {
        public EntityProvider RedPrefab;
        public EntityProvider BluePrefab;

        public int Rows;
        public int Cols;
        public float Spacing;

        public Vector3 RedOrigin;
        public Vector3 BlueOrigin;

        public Vector3 RedForward;
        public Vector3 BlueForward;
    }
}