using TileMap;
using UnityEngine;

namespace DefaultNamespace
{
    public class AI
    {
        [SerializeField] private Tower.Tower[] towers;
        
        private void BuildTower()
        {
            Tower.Tower tower = GetRandomTower();
            Tile tile = TileGrid.instance.TowerTiles[Random.Range(0, TileGrid.instance.TowerTiles.Count)];
            
            
        }

        private Tower.Tower GetRandomTower()
        {
            return towers[Random.Range(0,towers.Length)];
        }
    }
}