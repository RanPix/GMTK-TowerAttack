using System;
using System.Collections;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance { get; private set; }

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private TrackStore[] waypoints;
    [Space]
    [SerializeField] private Units units;

    private byte[,] unitTypeLines = new byte[UNIT_TYPE_LINES, UNIT_TYPE_LINE_SIZE];

    public const int UNIT_TYPE_LINES = 3;
    public const int UNIT_TYPE_LINE_SIZE = 70;

    private uint unitCount;

    private void Awake()
    {
        SetInstance();

        SetUpUnitLines();
    }

    private void Start()
    {
        RoundManager.Instance.OnRoundStart += SendNewRound;
    }

    private void SetUpUnitLines()
    {
        for (int y = 0; y < UNIT_TYPE_LINES; y++)
        {
            for (int x = 0; x < UNIT_TYPE_LINE_SIZE; x++)
            {
                unitTypeLines[y, x] = 255;
            }
        }
    }

    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("INSTANCE OF UNIT SPAWNER ALREADY EXISTS");
    }

    private IEnumerator SendWave()
    {
        for (int x = 0; x < UNIT_TYPE_LINE_SIZE; x++)
        {
            for (int y = 0; y < UNIT_TYPE_LINES; y++)
            {
                byte currentType = unitTypeLines[y, x];
                if (currentType == 255)
                {
                    yield return new WaitForSeconds(0.1f);
                    continue;
                }

                int yIndex = y % spawnPoints.Length;

                Instantiate(units.units[currentType], spawnPoints[yIndex].position, Quaternion.identity)
                    .GetComponent<UnitMovement>().SetWaypoints(waypoints[yIndex].Waypoints);

                unitCount = Math.Clamp(unitCount - 1, 0, 100000);

                yield return new WaitForSeconds(0.1f);
            }

            if (unitCount == 0)
                break;
        }
    }

    public void StartNextRound()
    {
        if (UnitList.UnitRoundCount > 0)
            return;

        GetUnitTypeLines();
        GetUnitCount();

        if (unitCount < 1)
            return;

        RoundManager.Instance.StartNewRound();
    }

    private void SendNewRound()
    {
        UnitList.SetCount(unitCount);
        StartCoroutine(SendWave());
    }

    private void GetUnitTypeLines()
        => unitTypeLines = BuyMenu.Instance.GetUnitTypeLines();

    private void GetUnitCount()
    {
        for (int x = 0; x < UNIT_TYPE_LINE_SIZE; x++)
        {
            for (int y = 0; y < UNIT_TYPE_LINES; y++)
            {
                if (unitTypeLines[y, x] != 255)
                    unitCount++;
            }
        }
    }

    private void OnDestroy()
    {
        RoundManager.Instance.RESET();
    }
}
