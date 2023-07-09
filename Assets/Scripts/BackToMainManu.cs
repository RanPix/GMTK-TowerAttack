using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMainManu : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }
}
