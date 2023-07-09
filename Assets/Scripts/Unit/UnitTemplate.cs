using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit Data")]
public class UnitTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public int Price;

    public UnitTypes Type;
    public int Damage;
    public float NormalSpeed;
    public float MaxHP;
}
