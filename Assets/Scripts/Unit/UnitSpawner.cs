using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    public static UnitSpawner Instance;

    private Transform spawnPoint;

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

    private void Update()
    {
        
    }

    public void SetUnitType(int lineIndex, int linePosition, byte unitType)
    {
        unitTypesLine[lineIndex, linePosition] = unitType;
    }

    private void ParseUnitTypesLine()
    {

    }
}
