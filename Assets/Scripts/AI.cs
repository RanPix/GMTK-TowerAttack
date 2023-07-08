using TileMap;
using UnityEngine;

namespace DefaultNamespace
{
    public class AI : MonoBehaviour
    {
        public static AI Instance { get; private set; }

        [SerializeField] private GameObject[] towers;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        public void BuildStartTowers()
        {
            for (int i = 0; i < 3; i++)
                BuildTower();
        }
        private void BuildTower()
        {
            GameObject tower = GetRandomTower();

            Tile tile = TileGrid.instance.TowerTiles[Random.Range(0, TileGrid.instance.TowerTiles.Count)];

            Instantiate(tower, tile.transform);
        }

        private GameObject GetRandomTower()
        {
            return towers[Random.Range(0,towers.Length)];
        }
    }
}