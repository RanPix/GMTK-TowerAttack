using UnityEngine;
using UnityEngine.UI;

public class UnitLineUIButton : MonoBehaviour
{
    [SerializeField] private Units units;

    [SerializeField] private Button button;
    [SerializeField] private Image unitImage;
    [SerializeField] private Sprite blankSprite;

    public int unitLine { get; private set; }

    public void Setup(int unitLinePosition, int unitLine)
    {
        this.unitLine = unitLine;

        button.onClick.AddListener(() 
            => BuyUnit(unitLinePosition));
    }

    private void BuyUnit(int unitLinePosition)
    {
        bool canBuy = BuyMenu.Instance.BuyUnit(unitLine, unitLinePosition);
        SetButtonUnitSprite(BuyMenu.Instance.SelectedUnitType, canBuy);
    }

    private void SetButtonUnitSprite(byte unitType, bool canBuy)
    {
        if (!canBuy)
            return;

        if (unitType == 255)
        {
            unitImage.sprite = blankSprite;
            return;
        }

        unitImage.sprite = units.units[unitType].GetComponent<SpriteRenderer>().sprite;
    }
}
