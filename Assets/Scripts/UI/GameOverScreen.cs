using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject youwinText;

    private void Start()
    {
        LevelStatsCounter.Instance.OnVictory += YouWin;
        RoundManager.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        Enable();
        gameoverText.SetActive(true);
    }

    private void YouWin()
    {
        Enable();
        gameoverText.SetActive(true);
    }

    private void Enable()
    {
        panel.SetActive(true);
    }
}
