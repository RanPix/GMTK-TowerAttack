using UnityEngine;

[CreateAssetMenu(fileName = "Units", menuName = "Units Data")]
public class Units : ScriptableObject
{
    public GameObject[] units;
    public UnitTemplate[] unitTemplates;
}
