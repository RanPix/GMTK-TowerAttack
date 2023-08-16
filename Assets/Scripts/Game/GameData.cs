using System;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameData : NetworkBehaviour
{
    
    private static GameData _Instance;
    public static GameData Instance
    {
        get => _Instance;
        set
        {
            if(!Instance)
                _Instance = value;
        }
    }

    [SyncVar] 
    public Player Attacker;

    [SyncVar] 
    public Player Defender;
    
    public Player localPlayer;

    public Action<Player> OnPlayerSet;


    [Server]
    public PlayerRole GetFreePlayerRole()
    {
        
        if ((!Attacker && !Defender))
            return (PlayerRole)Random.Range(0, 2);
        
        if(!Defender && Attacker)
            return PlayerRole.Defender;
        
        if (!Attacker && Defender)
            return PlayerRole.Attacker;
        
        return PlayerRole.None;
    }

    [Server]
    public void SetPlayer(Player player)
    {
        switch (player.PlayerRole)
        {
            case PlayerRole.Attacker:
                Attacker = player;
                break;
            
            case PlayerRole.Defender:
                Defender = player;
                break;
            
            default:
                throw new NotImplementedException();
        }
        OnPlayerSet?.Invoke(player);
        
        if(NetworkClient.localPlayer.gameObject == player.gameObject)
            localPlayer = player;
    }
}
