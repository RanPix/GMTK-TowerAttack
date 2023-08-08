using UnityEngine;
using Assets.Scripts.Unit.TagSystem;

[RequireComponent(typeof(UnitMovement), typeof(UnitTags))]
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

        var validationKey = new AudioValidationKey(AudioKind.Creeps, AudioType.CreepDeath, "Death");

        AudioSystem.Instance.PlaySound(validationKey, AudioKind.Creeps);

        Destroy(gameObject);
    }

    private void Explode()
    {
        Gate.Instance.DamageGate(unitData.Damage);
        Death();
    }

    private void OnDestroy()
    {
        GetComponent<UnitMovement>().OnPrelastPosition -= Explode;

        UnitList.RemoveObject();
    }
}
