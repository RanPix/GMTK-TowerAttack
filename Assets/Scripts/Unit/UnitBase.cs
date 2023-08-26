using UnityEngine;
using Assets.Scripts.Unit.TagSystem;

[RequireComponent(typeof(UnitMovement))]
public class UnitBase : MonoBehaviour
{
    public Health HP { get; private set; }
    public UnitTags Tags { get; private set; }

    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    [Space]
    [SerializeField] private GameObject DeathEffect;
    
    private void Awake()
    {
        UnitList.AddObject();

        Tags = new(unitData);

        HP = new(this);
        HP.Death += Death;

        GetComponent<UnitMovement>().OnPrelastPosition += Explode;
    }

    private void Death()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
        
        PlayDeathAudio();

        Destroy(gameObject);
    }

    private static void PlayDeathAudio()
    {
        var validationKey = new AudioValidationKey(AudioKind.Creeps, AudioType.CreepDeath, "Death");

        AudioSystem.Instance.PlaySound(validationKey, AudioKind.Creeps);
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
