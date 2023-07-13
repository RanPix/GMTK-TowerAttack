using UnityEngine;

[CreateAssetMenu(fileName = "Maps_Info", menuName = "Maps Data")]
public class MapsCollectionInfo : ScriptableObject
{
    public GameObject[] Maps;
    public Sprite[] MapPreviews;
    public string[] Names;
    public string[] Difficulties;
}
