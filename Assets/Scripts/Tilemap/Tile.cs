using System;
using UnityEngine;

namespace TileMap
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileType _type;

        public TileType type
        {
            get => _type;
            private set
            {
                _type = value;
                TypeProcessing();
            }
        }

        public Vector2Int gridPosition;

        public Action OnCreepEnter;
        
        public Vector2 Position
        {
            get
                => transform.position;

            set
                => transform.position = value;
        }

        public void Start()
        {
            TileGrid.instance.AddTile(this);
            TypeProcessing();
        }

        public void SetArgs(TileCreateArgs args)
        {
            type = args.type;
        }
        
        private void TypeProcessing()
        {
        }
    }
}