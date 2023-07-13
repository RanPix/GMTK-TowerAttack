using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainManu : MonoBehaviour
{
    [SerializeField] private Button button;
    [Space]
    [SerializeField] private Scene mainMenuScene;

    private void Start()
    {
        button.onClick.AddListener(() => SceneManager.LoadScene(mainMenuScene.name));
    }
}
