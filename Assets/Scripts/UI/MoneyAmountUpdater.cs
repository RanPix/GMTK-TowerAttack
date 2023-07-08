using TMPro;
using UnityEngine;

public class MoneyAmountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        PlayerData.OnMoneyUpdate += UpdateText;
        UpdateText();
    }
    
    private void UpdateText()
    {
        moneyText.text = PlayerData.Money.ToString() + "$";
    }
}
