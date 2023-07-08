using UnityEngine;

[RequireComponent(typeof(UnitMovement), typeof(UnitTags))]
public class UnitBase : MonoBehaviour
{
    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    public Health HP { get; private set; }

    private void Awake()
    {
        HP = new(this);
        HP.Death += Death;
        GetComponent<UnitMovement>().OnPrelastPosition += Explode;
    }

    private void Death()
    {
        Destroy(this);
    }

    private void Explode()
    {
        LevelStatsCounter.Instance.DamageGate(unitData.Damage);
        Death();
    }
}
