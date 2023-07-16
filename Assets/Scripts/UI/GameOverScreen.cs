using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject gameoverText;
    [SerializeField] private GameObject youwinText;

    private void Start()
    {
        Gate.Instance.OnVictory += YouWin;
        RoundManager.Instance.OnGameOver += GameOver;
    }

    private void GameOver()
    {
        EnablePanel();
        gameoverText.SetActive(true);
    }

    private void YouWin()
    {
        EnablePanel();
        gameoverText.SetActive(true);
    }

    private void EnablePanel()
        => panel.SetActive(true);
}
