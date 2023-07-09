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

            RoundManager.OnRoundStart += DoRoundStep;
        }

        private void OnDestroy()
        {
            RoundManager.OnRoundStart -= DoRoundStep;
        }

        public void DoRoundStep()
        {
            float waveNumber = RoundManager.RoundCount;

            for (int i = 0; i < waveNumber + waveNumber * 0.1f; i++)
                BuildTower();
        }

        private void BuildTower()
        {
            (int, GameObject) towerPair = GetTower();

            GetTile(towerPair);
        }

        private (int, GameObject) GetTower()
        {
            float waveNumber = RoundManager.RoundCount;

            var tower = GetRandomTower((int)Mathf.Round(waveNumber * 0.55f), waveNumber * 0.1f);

            return tower;
        }

        private void GetTile((int tier, GameObject tower) towerPair)
        {
            var towerTiles = TileGrid.instance.TowerTiles;

            if (AllCellsAreOccupied())
            {
                var randomTile = GetWeakestTile(towerPair.tier, towerTiles);

                randomTile.DestroyTower();

                randomTile.CreateTower(towerPair.tower);
            }

            TowerTile chosenTile;

            do
            {
                chosenTile = towerTiles[Random.Range(0, towerTiles.Count)];
            } while (chosenTile.IsOccupied);

            chosenTile.CreateTower(towerPair.tower);
        }

        private TowerTile GetWeakestTile(int tier, List<TowerTile> tiles)
        {
            TowerTile chosenTile = null;

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
            wantedTier = Mathf.Clamp(wantedTier, 0, 6);

            List<(int, GameObject)> neededTierTowers = new List<(int, GameObject)>();

            if (Random.Range(0.1f, 1f) < chanceToGetLowerTier)
                wantedTier = GetRandomTier(wantedTier);

            foreach (var tower in towers)
            {
                var towerComponent = tower.GetComponent<Tower>();

                if (towerComponent.TowerTier == wantedTier)
                    neededTierTowers.Add((towerComponent.TowerTier, tower));
            }

            if(neededTierTowers.Count > 0)
            {
                return neededTierTowers[Random.Range(0, neededTierTowers.Count)];
            }
            else
            {
                var simpleTower = towers[0];

                return (1, simpleTower);
            }
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