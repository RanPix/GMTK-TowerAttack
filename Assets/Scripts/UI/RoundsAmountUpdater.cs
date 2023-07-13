using TMPro;
using UnityEngine;

public class RoundsAmountUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Start()
    {
        RoundManager.Instance.OnRoundStart += UpdateText;
        UpdateText();
    }

    private void UpdateText()
        => text.text = RoundManager.Instance.RoundCount + " / " + LevelStatsCounter.Instance.MaxWave;

    private void OnEnable()
    {
        UpdateText();
    }
}
