using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitCountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;

    private void Start()
    {
        UnitList.OnUnitRoundCountChange += UpdateText;
        RoundManager.OnRoundStart += UpdateMaxUnits;

        UpdateText();
    }
    private void OnDestroy()
    {
        UnitList.OnUnitRoundCountChange -= UpdateText;
    }
    private void UpdateText()
    {
        text.text = UnitList.UnitRoundCount.ToString();
        slider.value = UnitList.UnitRoundCount;
    }

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateMaxUnits()
    {
        slider.maxValue = UnitList.UnitRoundCount;
        UpdateText();
    }
}
