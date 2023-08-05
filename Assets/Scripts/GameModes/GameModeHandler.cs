using System;
using UnityEngine;

public class GameModeHandler : MonoBehaviour
{
    private static GameModeHandler _Instance;
    public static GameModeHandler Instance
    {
        get => _Instance;
        private set
        {
            if (_Instance == null)
                _Instance = value;
            
        } 
    }

    private GameModeInfo _chosenGameMode;
    public GameModeInfo chosenGameMode
    {
        get => _chosenGameMode;
        set
        {
            _chosenGameMode = value;
            OnGameModeChanged?.Invoke(value);
        }
    }
    
    public Action<GameModeInfo> OnGameModeChanged;

    private void Awake()
    {
        SetInstance();
        DontDestroyOnLoad(gameObject);
    }

    private void SetInstance()
    {
        Instance = this;
    }
}
