using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateHealthUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;

    private void Start()
    {
        LevelStatsCounter.Instance.OnGateHealthChanged += UpdateText;
        slider.maxValue = LevelStatsCounter.Instance.GateHealth;

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = LevelStatsCounter.Instance.GateHealth.ToString();
        slider.value = LevelStatsCounter.Instance.GateHealth;
    }

    private void OnEnable()
    {
        UpdateText();
    }
}
