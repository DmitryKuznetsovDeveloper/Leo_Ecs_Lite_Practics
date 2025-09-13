using System;
using ECS.Services;
using UnityEngine;

namespace ECS.Components
{
    [Serializable]
    public struct BulletWeapon
    {
        public Transform FirePoint;
        public EntityProvider BulletPrefab;
    }
}