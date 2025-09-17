using ECS.Components;
using ECS.Services;
using UnityEngine;

namespace ECS.Views
{
    public sealed class ArmySpawnConfigProvider : EntityProvider
    {
        [SerializeField] private EntityProvider _redPrefab;
        [SerializeField] private EntityProvider _bluePrefab;
        [SerializeField] private int _rows = 10;
        [SerializeField] private int _cols = 10;
        [SerializeField] private float _spacing = 2f;
        [SerializeField] private Vector3 _redOrigin = new(-20f, 0f, -10f);
        [SerializeField] private Vector3 _blueOrigin = new(20f, 0f, 10f);
        [SerializeField] private Vector3 _redForward = Vector3.right;
        [SerializeField] private Vector3 _blueForward = Vector3.left;

        public override void Install(Entity entity)
        {
            entity.AddComponent(new ArmySpawnConfig
            {
                RedPrefab = _redPrefab,
                BluePrefab = _bluePrefab,
                Rows = _rows,
                Cols = _cols,
                Spacing = _spacing,
                RedOrigin = _redOrigin,
                BlueOrigin = _blueOrigin,
                RedForward = _redForward,
                BlueForward = _blueForward
            });
        }
    }
}