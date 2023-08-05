using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "ScriptableObjects/GameModeInfo", fileName = "GameModeInfo")]
public class GameModeInfo : ScriptableObject
{
    public GameObject ModePrefab;
    [Space]
    public bool Multiplayer;
    public GameModes GameMode;
    [Space]
    public string ModeName;
    public string Description;
    [Space]
    public Texture ModeIcon;
    public int RequirePlayersAmount;
}
