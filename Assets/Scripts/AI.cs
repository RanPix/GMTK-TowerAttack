using TileMap;
using UnityEngine;

namespace DefaultNamespace
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private GameObject[] towers;

        public void Start()
        {
            for (int i = 0; i < 6; i++)
                BuildTower();
        }
        private void BuildTower()
        {
            GameObject tower = GetRandomTower();

            Transform tile = TileGrid.instance.TowerTiles[Random.Range(0, TileGrid.instance.TowerTiles.Count)];

            Instantiate(tower, tile.position, Quaternion.identity);
        }

        private GameObject GetRandomTower()
        {
            return towers[Random.Range(0,towers.Length)];
        }
    }
}