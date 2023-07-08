using UnityEngine;

public class BuyMenuBuilder : MonoBehaviour
{
    [SerializeField] private Units units;
    [SerializeField] private GameObject buyUnitButton;
    [SerializeField] private UnitDescription unitDescription;

    private void Start()
    {
        for (byte i = 0; i < units.units.Length; i++)
        {
            Instantiate(buyUnitButton, transform).GetComponent<BuyUnitButton>()
                .SetSellingUnit(i, units.units[i], units.unitTemplates[i], unitDescription);
        }

        Instantiate(buyUnitButton, transform).GetComponent<BuyUnitButton>()
                .SetSellingUnit(255, null, null, null);
    }
}
