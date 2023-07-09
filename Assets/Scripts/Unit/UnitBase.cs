using UnityEngine;

[RequireComponent(typeof(UnitMovement), typeof(UnitTags))]
public class UnitBase : MonoBehaviour
{
    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    public Health HP { get; private set; }
    
    private void Awake()
    {
        UnitList.AddObject();

        HP = new(this);
        HP.Death += Death;
        GetComponent<UnitMovement>().OnPrelastPosition += Explode;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void Explode()
    {
        LevelStatsCounter.Instance.DamageGate(unitData.Damage);
        Death();
    }

    private void OnDestroy()
    {
        UnitList.RemoveObject();
    }
}
