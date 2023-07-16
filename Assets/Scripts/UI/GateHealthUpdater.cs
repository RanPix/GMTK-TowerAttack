using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GateHealthUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;

    private void Start()
    {
        Gate.Instance.OnGateHealthChanged += UpdateText;
        slider.maxValue = Gate.Instance.GateHealth;

        UpdateText();
    }

    private void UpdateText()
    {
        text.text = Gate.Instance.GateHealth.ToString();
        slider.value = Gate.Instance.GateHealth;
    }

    private void OnEnable()
    {
        UpdateText();
    }
}
