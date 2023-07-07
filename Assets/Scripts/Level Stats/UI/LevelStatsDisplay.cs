using UnityEngine;
using TMPro;

public class LevelStatsDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text GateHealthText;
    [SerializeField] private TMP_Text WaveNumberText;
    [SerializeField] private TMP_Text PassedTimeText;

    private void Start()
    {
        var instance = LevelStatsCounter.Instance;

        instance.OnGateHealthChanged += ChangeGateHealthText;
        instance.OnTimeChanged += ChangePassedTime;
        instance.OnWaveChanged += ChangeWaveNumberText;
    }
    private void ChangeGateHealthText()
        => GateHealthText.text = LevelStatsCounter.Instance.GateHealth.ToString();
    private void ChangeWaveNumberText()
        => WaveNumberText.text = LevelStatsCounter.Instance.WaveNumber.ToString();
    private void ChangePassedTime()
        => WaveNumberText.text = LevelStatsCounter.Instance.PassedTime.ToString();
}
