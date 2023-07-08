using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TrackStore[] waypoints;

    [SerializeField] private Units units;

    private byte[,] unitTypesLine = new byte[3, 30];
    

    private void Awake()
    {
        int xLen = unitTypesLine.GetLength(1);
        int yLen = unitTypesLine.GetLength(0);

        for (int y = 0; y < yLen; y++)
        {
            for (int x = 0; x < xLen; x++)
            {
                unitTypesLine[y, x] = 255;
            }
        }

        if (Instance == null)
            Instance = this;

        else
            Debug.LogError("INSTANCE OF UNIT SPAWNER ALREADY EXISTS");
    }

    private IEnumerator SendRound()
    {
        int xLen = unitTypesLine.GetLength(1);
        int yLen = unitTypesLine.GetLength(0);
        
        for (int x = 0; x < xLen; x++)
        {
            for (int y = 0; y < yLen; y++)
            {
                byte currentType = unitTypesLine[y, x];
                if (currentType == 255)
                {
                    yield return new WaitForSeconds(0.25f);
                    continue;
                }

                GameObject unit = Instantiate(units.units[currentType], spawnPoints[y].position, Quaternion.identity);
                unit.GetComponent<UnitMovement>().SetWaypoints(waypoints[y].Waypoints);

                yield return new WaitForSeconds(0.25f);
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StartNextRound()
    {
        GetUnitTypeLines();
        StartCoroutine(SendRound());
    }

    public void GetUnitTypeLines()
    {
        unitTypesLine = BuyMenu.Instance.GetUnitTypeLines();
    }
}
