using System;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [field: SerializeField] public PlayerData PlayerData { get; private set; }

    [field: SyncVar]
    public PlayerRole PlayerRole { get; private set; }
    
    private void Start()
    {
        SetRole();
        
        GameData.Instance.SetPlayer(this);
    }

    [Server]
    private void SetRole()
    {
        PlayerRole = GameData.Instance.GetFreePlayerRole();
    }
}
