using System;
using Mirror;
using UnityEngine;

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
}
