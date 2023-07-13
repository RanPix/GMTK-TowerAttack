using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyUnitButton : MonoBehaviour
{
    [SerializeField] private byte sellingUnit;

    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text unitCostText;

    private UnitDescription unitDescription;

    private GameObject unitPrefab;
    private UnitTemplate unitTemplate;

    private void Start()
    {
        button.onClick.AddListener(() => { BuyMenu.Instance.ChooseUnit(sellingUnit); UpdateDescription(); });

        CheckUnit();
    }

    private void CheckUnit()
    {
        if (sellingUnit == 255)
            return;

        image.sprite = unitPrefab.GetComponent<SpriteRenderer>().sprite;

        unitCostText.text = unitTemplate.Price.ToString() + "$";
    }

    private void UpdateDescription()
    {
        if (unitTemplate != null)
            unitDescription.SetInfo(unitTemplate);
    }

    public void SetSellingUnit(byte sellingUnit, GameObject unitPrefab, UnitTemplate template, UnitDescription unitDescription)
    {
        this.sellingUnit = sellingUnit;

        if (sellingUnit == 255)
        {
            unitCostText.text = "";
            return;
        }

        this.unitTemplate = template;
        this.unitPrefab = unitPrefab;

        this.unitDescription = unitDescription;
    }
}
