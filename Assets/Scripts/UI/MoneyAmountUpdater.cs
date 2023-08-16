using TMPro;
using UnityEngine;

public class MoneyAmountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void Start()
    {
        GameData.Instance.localPlayer.PlayerData.OnMoneyUpdate += UpdateText;
        UpdateText();
    }
    
    private void UpdateText()
        => moneyText.text = GameData.Instance.localPlayer.PlayerData.Money.ToString() + "$";

    private void OnEnable()
        => UpdateText();
}
