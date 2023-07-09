using TMPro;
using UnityEngine;

public class RoundsAmountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        RoundManager.OnRoundStart += UpdateText;
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = RoundManager.RoundCount + " / " + LevelStatsCounter.Instance.MaxWave;
    }

    private void OnEnable()
    {
        UpdateText();
    }
}
