using AI;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace TileMap
{
    public class TileGrid : MonoBehaviour
    {
        public static TileGrid Instance { get; private set; }

        public List<TowerTile> TowerTiles;

        [SerializeField] private Tilemap tilemap;
        [SerializeField] private TileBase[] tilemapBaseTiles;

        [SerializeField] private GameObject tilePrefab;

        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Tile[,] tiles;
        [SerializeField] private float tileSize;
        [SerializeField] private Vector2 origin;
        
        [SerializeField] private List<TileType> tileTypes;

        [SerializeField] private List<Transform> BlankTowerTiles;

        private void Awake()
        {
            SetInstance();

            tiles = new Tile[gridSize.x, gridSize.y];

            CreateTowerTiles();
        }

        private void SetInstance()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError("TILE GRID INSTANCE ALREADY EXISTS");
        }
        private void CreateTowerTiles()
        {
            foreach(var tower in BlankTowerTiles)
            {
                var newTowerTile = (TowerTile)ScriptableObject.CreateInstance(typeof(TowerTile));

                newTowerTile.SetTile(tower);

                TowerTiles.Add(newTowerTile);
            }
        }

        private void Start()
        {
            BuildTilemap();
        }

        public Tile[,] GetTiles() => tiles;

        public Tile GetTile(Vector2 tilePosition)
        {
            Vector2Int tileIndex = GetTileXY(tilePosition);
            return tiles[tileIndex.x, tileIndex.y];
        }

        public TowerTile GetFreeTowerTile()
        {
            TowerTile tile = TowerTiles[0];

            do
            {
                tile = TowerTiles[Random.Range(0, TowerTiles.Count)];
            }
            while (tile.IsOccupied);

            return tile;
        }

        private Vector2Int GetTileXY(Vector2 worldPosition)
        {
            return new Vector2Int(Mathf.FloorToInt((worldPosition.x - origin.x) / tileSize),
                                  Mathf.FloorToInt((worldPosition.y - origin.y) / tileSize));
        }

        public Vector2 GetTileWorldPos(Vector2 tilePos)
        {
            return tilePos + origin + new Vector2(tileSize, tileSize) * 0.5f;
        }

        public void AddTile(Tile spawnTile, TileType tileType)
        {
            Vector2Int gridSpawnPosition = GetTileXY(spawnTile.Position);

            spawnTile.Position = GetTileWorldPos(gridSpawnPosition);
            spawnTile.GridPosition = gridSpawnPosition;

            tiles[gridSpawnPosition.x, gridSpawnPosition.y] = spawnTile;

            BuildTilemap();
        }
        
        private void BuildTilemap()
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y ; y++) 
                {
                    if (tiles[x, y])
                        tilemap.SetTile(new Vector3Int(x, y, 0) - new Vector3Int(gridSize.x, gridSize.y, 0) / 2, GetTileBase(tiles[x, y].Type));
                }
            }
        }

        private TileBase GetTileBase(TileType type)
        {
            if ((int)type < tilemapBaseTiles.Length)
                return tilemapBaseTiles[(int)type];

            else
                return tilemapBaseTiles[0];
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}