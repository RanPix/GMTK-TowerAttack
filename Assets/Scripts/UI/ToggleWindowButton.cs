using UnityEngine;

public class ToggleWindowButton : MonoBehaviour
{
    [SerializeField] private GameObject toggleWindow;
    [Space]
    [SerializeField] private bool needToClosePreviousWindow;

    public void OpenWindow()
    {
        if(needToClosePreviousWindow)
            MainMenuWindowsManager.Instance?.OpenWindow(toggleWindow);
        else
            toggleWindow.SetActive(true);
    }

    public void CloseWindow()
    {
        toggleWindow.SetActive(false);
    }
}
