using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    public static BuyMenu Instance;

    [SerializeField] private Units units;

    public byte SelectedUnitType { get; private set; }

    private byte[,] unitTypeLines = new byte[3, 30];

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogError("INSTANCE OF BUY MENU CURSOR DATA ALREADY EXISTS");

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
    {
        SelectedUnitType = unitType;
    }

    public bool BuyUnit(int lineIndex, int linePosition) 
    {
        byte existingUnitType = unitTypeLines[lineIndex, linePosition];

        if (existingUnitType == SelectedUnitType)
            return false;

        if (SelectedUnitType == 255)
        {
            PlayerData.Money += units.unitTemplates[existingUnitType].Price;
            unitTypeLines[lineIndex, linePosition] = SelectedUnitType;
            return true;
        }    

        int selectedUnitPrice = units.unitTemplates[SelectedUnitType].Price;

        if (PlayerData.Money >= selectedUnitPrice)
        {
            if (existingUnitType != 255)
                PlayerData.Money += units.unitTemplates[existingUnitType].Price;

            PlayerData.Money -= selectedUnitPrice;
            unitTypeLines[lineIndex, linePosition] = SelectedUnitType;

            return true;
        }

        return false;
    }

    public bool BuyNewUnitLine(int price)
    {
        if (PlayerData.Money >= price)
        {
            PlayerData.Money -= price;
            return true;
        }

        return false;
    }

    public byte[,] GetUnitTypeLines()
        => unitTypeLines;
}
