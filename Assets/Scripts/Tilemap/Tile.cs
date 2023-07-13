using UnityEngine;

namespace TileMap
{
    public class Tile : MonoBehaviour
    {
        [field : SerializeField] public TileType Type { get; private set;}

        [HideInInspector] public Vector2Int GridPosition;
        
        public Vector2 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        private void Start()
        {
            TileGrid.Instance.AddTile(this, Type);
        }
    }
}