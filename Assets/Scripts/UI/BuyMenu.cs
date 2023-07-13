using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    public static BuyMenu Instance { get; private set; }

    [SerializeField] private Units units;

    public byte SelectedUnitType { get; private set; }

    private byte[,] unitTypeLines = new byte[UnitSpawner.UNIT_TYPE_LINES, UnitSpawner.UNIT_TYPE_LINE_SIZE];

    private void Awake()
    {
        SetInstance();

        InitializeUnitLines();
    }
    
    private void SetInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("INSTANCE OF BUY MENU CURSOR DATA ALREADY EXISTS");

    }

    private void InitializeUnitLines()
    {
        int yLen = unitTypeLines.GetLength(0);
        int xLen = unitTypeLines.GetLength(1);

        for (int y = 0; y < yLen; y++)
        {
            for (int x = 0; x < xLen; x++)
            {
                unitTypeLines[y, x] = 255;
            }
        }
    }

    public void ChooseUnit(byte unitType)
        => SelectedUnitType = unitType;

    public bool BuyUnit(int lineIndex, int linePosition) 
    {
        byte existingUnitType = unitTypeLines[lineIndex, linePosition];

        if (existingUnitType == SelectedUnitType)
            return false;

        int selectedUnitPrice = units.unitTemplates[SelectedUnitType].Price;

        if (PlayerData.Money < selectedUnitPrice)
            return false;

        ReplaceUnit(existingUnitType, lineIndex, linePosition);

        PlayerData.Money -= selectedUnitPrice;
        unitTypeLines[lineIndex, linePosition] = SelectedUnitType;

        return true;
    }

    // замініти наявний продаж новим, нормальним
    private void ReplaceUnit(byte existingUnitType, int lineIndex, int linePosition)
    {
        if (SelectedUnitType != 255)
            return;

        PlayerData.Money += units.unitTemplates[existingUnitType].Price;
        unitTypeLines[lineIndex, linePosition] = SelectedUnitType;
    }

    public bool BuyNewUnitLine(int price)
    {
        if (PlayerData.Money < price)
            return false;

        PlayerData.Money -= price;
        return true;
    }

    public byte[,] GetUnitTypeLines()
        => unitTypeLines;

    private void OnDestroy()
    {
        Instance = null;
    }
}
