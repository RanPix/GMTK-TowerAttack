using System;
using UnityEngine;

public class MainMenuWindowsManager : MonoBehaviour
{
    public static MainMenuWindowsManager Instance
    {
        get => _Instance;
        private set
        {
            if (_Instance == null)
                _Instance = value;
        } 
    }
    private static MainMenuWindowsManager _Instance;
    
    private GameObject openedWindow;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenWindow(GameObject openedWindow)
    {
        if(this.openedWindow)
            this.openedWindow.SetActive(false);
        
        this.openedWindow = openedWindow;
        
        if(this.openedWindow)
            this.openedWindow.SetActive(true);
    }
}
