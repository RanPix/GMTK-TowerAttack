using UnityEngine;

[CreateAssetMenu(fileName = "MapInfo", menuName = "ScriptableObjects/Maps Data/Map Info")]
public class MapInfo : ScriptableObject
{
    public GameObject Map;
    public Sprite MapPreview;
    public string MapName;
    public string MapDifficulty;
}
