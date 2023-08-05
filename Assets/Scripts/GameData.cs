using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameData : MonoBehaviour
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

    public Player Attacker;
    public Player Defender;
}
