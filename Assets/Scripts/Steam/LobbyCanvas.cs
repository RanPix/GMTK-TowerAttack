using System;
using Mirror;
using UnityEngine;

public class LobbyCanvas : NetworkBehaviour
{
    public PlayerCard ownCard { get; private set; }
    public PlayerCard enemyCard { get; private set; }

    private void Start()
    {
        
    }
}
