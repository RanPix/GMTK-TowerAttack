using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit Data")]
public class UnitTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public int price;

    public UnitTypes Type;
    public int Damage;
    public float Speed;
    public float MaxHP;
}
