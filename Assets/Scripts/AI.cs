using System.Collections.Generic;
using TileMap;
using UnityEngine;
using Towers;
using System;
using Random = UnityEngine.Random;

namespace AI
{
    public class AI : MonoBehaviour
    {
        [SerializeField] private GameObject[] towers;
        [Space]
        [SerializeField] private int startTowersCount = 4;
        [Space]
        [SerializeField] private bool automaticDistribution = true;
        [SerializeField] private Dictionary<int, int> towersToBuildOnWave = new();

        private void Start()
        {
            for (int i = 0; i < startTowersCount; i++)
                BuildTower();

            RoundManager.Instance.OnRoundStart += DoRoundStep;
        }

        private void OnDestroy()
        {
            RoundManager.Instance.OnRoundStart -= DoRoundStep;
        }

        #region Round step

        public void DoRoundStep()
        {
            int towersToBuild = TowersToBuild(RoundManager.Instance.RoundCount);

            for (int i = 0; i <= towersToBuild; i++)
                BuildTower();
        }

        private int TowersToBuild(int wave)
        {
            if (automaticDistribution)
                return AutomaticAmountOfTowersToBuild(wave);

            if (towersToBuildOnWave.Count < startTowersCount)
            {
#if DEBUG
                throw new IndexOutOfRangeException();
#endif

                return AutomaticAmountOfTowersToBuild(wave);
            }

            return towersToBuildOnWave[wave];

        }

        private int AutomaticAmountOfTowersToBuild(int wave)
            => (int)(wave * 0.5f);

        private void BuildTower()
        {
            (int, GameObject) towerPair = GetTowerPair();

            BuildOnTile(towerPair);
        }

        private (int, GameObject) GetTowerPair()
        {
            int waveNumber = RoundManager.Instance.RoundCount;

            var tower = GetRandomTower(Mathf.RoundToInt(waveNumber * 0.55f), waveNumber * 0.1f);

            return tower;
        }

        #endregion

        #region Building on tile

        private void BuildOnTile((int tier, GameObject tower) towerPair)
        {
            var towerTiles = TileGrid.Instance.TowerTiles;

            if (AllCellsAreOccupied())
                RebuildWeakestTower(towerPair, towerTiles);
            else
                TileGrid.Instance.GetFreeTowerTile()
                    .CreateTower(towerPair.tower);
        }

        private void RebuildWeakestTower((int tier, GameObject tower) towerPair, List<TowerTile> towerTiles)
        {
            var randomTile = GetWeakestTile(towerPair.tier, towerTiles);

            randomTile.DestroyTower();

            randomTile.CreateTower(towerPair.tower);
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
            foreach(var towerCell in TileGrid.Instance.TowerTiles)
            {
                if (!towerCell.IsOccupied)
                    return false;
            }

            return true;
        }

        #endregion

        #region Getting tower

        private (int, GameObject) GetRandomTower(int wantedTier, float chanceToGetLowerTier)
        {
            wantedTier = Mathf.Clamp(wantedTier, 0, 6);

            wantedTier = TryGetLowerTier(wantedTier, chanceToGetLowerTier);

            var neededTierTowers = GetNeededTowersTier(wantedTier);

            return GetSuitableTower(neededTierTowers);
        }

        private (int, GameObject) GetSuitableTower(List<(int, GameObject)> neededTierTowers)
        {
            if (neededTierTowers.Count > 0)
            {
                return neededTierTowers[Random.Range(0, neededTierTowers.Count)];
            }
            else
            {
#if DEBUG
                Debug.LogWarning("No suitable tower", this);
#endif

                return (1, towers[0]);
            }
        }

        private int TryGetLowerTier(int wantedTier, float chanceToGetLowerTier)
        {
            if (Random.Range(0.1f, 1f) < chanceToGetLowerTier)
                wantedTier = Random.Range(1, wantedTier);

            return wantedTier;
        }

        private List<(int, GameObject)> GetNeededTowersTier(int wantedTier)
        {
            List<(int, GameObject)> neededTierTowers = new();

            foreach (var tower in towers)
            {
                var towerTier = tower.GetComponent<Tower>().TowerTier;

                if (towerTier == wantedTier)
                    neededTierTowers.Add((towerTier, tower));
            }

            return neededTierTowers;
        }

        #endregion
    }
}