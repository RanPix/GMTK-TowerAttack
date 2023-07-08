using TMPro;
using UnityEngine;

public class UnitDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text descriptionText;
    [SerializeField] private TMP_Text typeText;
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private TMP_Text speedText;
    [SerializeField] private TMP_Text maxHPText;

    public void SetInfo(UnitTemplate template)
    {
        nameText.text = template.Name;
        descriptionText.text = template.Description;
        typeText.text = "Type: " + template.Type.ToString();
        damageText.text = "Dmg: " + template.Damage.ToString();
        speedText.text = "Spd: " + template.Speed.ToString();
        maxHPText.text = "HP: " + template.MaxHP.ToString();
    }
}
