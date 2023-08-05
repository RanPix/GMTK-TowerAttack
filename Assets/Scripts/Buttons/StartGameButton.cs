using Mirror;
using Steamworks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    private Button button;

    private bool _active = true;
    private bool active
    {
        get => _active;
        set
        {
            _active = value;
            button.interactable = value;
        }
    }

    private void Start()
    {
        button = GetComponent<Button>();
        GameModeHandler.Instance.OnGameModeChanged += CheckActive;
    }
    
    private void CheckActive(GameModeInfo info)
    {
        if (info.Multiplayer)
        {
            active = SteamManager.Initialized;
            return;   
        }

        active = true;
    }
    
    private void OnDestroy()
    {
        GameModeHandler.Instance.OnGameModeChanged -= CheckActive;
    }

    public void StartGame()
    {
        if (PickedMode.Info.Multiplayer)
        {
            SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, NetworkManager.singleton.maxConnections);
        }
        else
        {
            LoadScene();
        }
    
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
