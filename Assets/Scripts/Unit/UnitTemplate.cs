using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit Data")]
public class UnitTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public int Price;

    public UnitTypes Type;
    public float Damage;
    public float Speed;
    public float MaxHP;
}
