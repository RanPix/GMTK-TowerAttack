using UnityEngine;

[RequireComponent(typeof(UnitMovement), typeof(UnitTags), (typeof(AudioSource)))]
public class UnitBase : MonoBehaviour
{
    public Health HP { get; private set; }

    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    [Space]
    [SerializeField] private GameObject DeathEffect;
    
    private void Awake()
    {
        UnitList.AddObject();

        HP = new(this);
        HP.Death += Death;
        GetComponent<UnitMovement>().OnPrelastPosition += Explode;
    }

    private void Death()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);

        DeathSoundPlayer.Instance?.PlayDeathSound();

        Destroy(gameObject);
    }

    private void Explode()
    {
        LevelStatsCounter.Instance.DamageGate(unitData.Damage);
        Death();
    }

    private void OnDestroy()
    {
        GetComponent<UnitMovement>().OnPrelastPosition -= Explode;

        UnitList.RemoveObject();
    }
}
