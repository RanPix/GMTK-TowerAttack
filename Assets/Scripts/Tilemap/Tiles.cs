using System.Collections.Generic;
using UnityEngine;

namespace TileMap
{
    public class Tiles : MonoBehaviour
    {
        public static Tiles Instance { get; private set; }

        public List<TowerTile> TowerTiles { get; private set; } = new List<TowerTile>();

        [SerializeField] private List<Transform> BlankTowerTiles;

        private void Awake()
        {
            SetInstance();

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
            foreach (var tower in BlankTowerTiles)
            {
                var newTowerTile = (TowerTile)ScriptableObject.CreateInstance(typeof(TowerTile));

                newTowerTile.SetTile(tower);

                TowerTiles.Add(newTowerTile);
            }
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
        
        private void OnDestroy()
        {
            Instance = null;
        }
    }
}