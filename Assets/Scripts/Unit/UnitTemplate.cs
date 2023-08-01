using UnityEngine;
using Assets.Scripts.Unit.UnitTypes;

[CreateAssetMenu(fileName = "Unit", menuName = "Unit Data")]
public class UnitTemplate : ScriptableObject
{
    public string Name;
    public string Description;

    public int Price;

    public UnitStatus Type;
    public int Damage;
    public float NormalSpeed;
    public float MaxHP;
}
