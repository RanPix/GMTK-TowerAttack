using UnityEngine;

[CreateAssetMenu(fileName = "Maps_Info", menuName = "Maps Data")]
public class MapsCollectionInfo : ScriptableObject
{
    public GameObject[] maps;
    public Sprite[] mapPreviews;
    public string[] names;
    public string[] difficulties;
}
