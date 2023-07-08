using System.Collections.Generic;
using TileMap;
using UnityEngine;
using Towers;

namespace DefaultNamespace
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private GameObject[] towers;

        private void Start()
        {
            for (int i = 0; i < 6; i++)
                BuildTower();
        }

        public void DoRoundStep()
        {
            float waveNumber = LevelStatsCounter.Instance.WaveNumber;

            for (int i = 0; i < waveNumber + waveNumber * 0.2f; i++)
                BuildTower();
        }

        private void BuildTower()
        {
            (int, GameObject) towerPair = GetTower();

            Transform tile = GetTile(towerPair.Item1);

            Instantiate(towerPair.Item2, tile.position, Quaternion.identity);
        }

        private (int, GameObject) GetTower()
        {
            float waveNumber = LevelStatsCounter.Instance.WaveNumber;

            var tower = GetRandomTower((int)Mathf.Round(waveNumber * 0.5f), waveNumber * 0.1f);

            return tower;
        }

        private Transform GetTile(int tier)
        {
            Transform tile;

            var towerTiles = TileGrid.instance.TowerTiles;

            if (AllCellsAreOccupied())
            {
                var randomTile = GetWeakestTile(tier, towerTiles);

                randomTile.DestroyTower();

                tile = randomTile.TowerTransform;

                return tile;
            }

            TowerTile chosenTile;

            do
            {
                chosenTile = towerTiles[Random.Range(0, towerTiles.Count)];
            } while (chosenTile.IsOccupied);

            return chosenTile.transform;
        }

        private TowerTile GetWeakestTile(int tier, List<TowerTile> tiles)
        {
            TowerTile chosenTile = new TowerTile();

            foreach(var tile in tiles)
            {
                var tower = tile.CurrentTower.GetComponent<Tower>();

                if(tower.TowerTier <= tier)
                {
                    tier = tower.TowerTier;
                    chosenTile = tile;
                }
            }

            return chosenTile;
        }

        private bool AllCellsAreOccupied()
        {
            foreach(var towerCell in TileGrid.instance.TowerTiles)
            {
                if (!towerCell.IsOccupied)
                    return false;
            }

            return true;
        }

        private (int, GameObject) GetRandomTower(int wantedTier, float chanceToGetLowerTier)
        {            
            List<(int, GameObject)> neededTierTowers = new List<(int, GameObject)>();

            if (Random.Range(0.1f, 1f) < chanceToGetLowerTier)
                wantedTier = GetRandomTier(wantedTier);

            foreach (var tower in towers)
            {
                var towerComponent = tower.GetComponent<Tower>();

                if (towerComponent.TowerTier == wantedTier)
                    neededTierTowers.Add((towerComponent.TowerTier, tower));
            }

            return neededTierTowers[Random.Range(0, neededTierTowers.Count)];
        }

        private int GetRandomTier(int tier)
        {
            int memoryTier = tier;

            do
            {
                tier = Random.Range(1, memoryTier);
            }
            while (memoryTier == tier);

            return tier;
        }
        /*private float GetPercentValue(float number, float percent)
            => number * percent;*/
    }
}