using Steamworks;
using UnityEngine;

public class AddPlayerToLobbyButton : MonoBehaviour
{
    public void OpenOverlay()
    {
        SteamFriends.ActivateGameOverlayToUser("friendrequestaccept", SteamLobby.Instance.steamIDLobby);
    }
}
