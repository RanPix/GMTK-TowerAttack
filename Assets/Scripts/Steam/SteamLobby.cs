using UnityEngine;
using Mirror;
using Steamworks;

public class SteamLobby : MonoBehaviour
{
    private static SteamLobby _Instance;

    public static SteamLobby Instance
    {
        get => _Instance;
        set
        {
            if (!Instance)
                _Instance = value;
        }
    }
    
    private NetworkManager _networkManager 
        => NetworkManager.singleton;

    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject lobbyCanvas;

    public CSteamID steamIDLobby { get; private set; }

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    
    
    private const string HostAddressKey = "HostAddress";

    private void Start()
    {
        SetInstance();
        
        if(!SteamManager.Initialized)
            return;
        
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    private void SetInstance()
    {
        Instance = this;
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
            return;
        
        _networkManager.StartHost();
        
        mainMenuCanvas.SetActive(false);
        lobbyCanvas.SetActive(true);
        
        steamIDLobby = new CSteamID(callback.m_ulSteamIDLobby);
        SteamMatchmaking.SetLobbyData(steamIDLobby, HostAddressKey, 
            SteamUser.GetSteamID().ToString());
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
        
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        
        if (NetworkServer.active)
            return;
        
        steamIDLobby = new CSteamID(callback.m_ulSteamIDLobby);
        string hostAddress = SteamMatchmaking.GetLobbyData(steamIDLobby, HostAddressKey);
        _networkManager.networkAddress = hostAddress;
        _networkManager.StartClient();
        
        
    }
}