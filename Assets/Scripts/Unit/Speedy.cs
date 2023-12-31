using System.Collections;
using UnityEngine;
using Assets.Scripts.Unit.TagSystem;

public class Speedy : MonoBehaviour
{
    [SerializeField] private float speedBuffDuration = 1.5f;
    [SerializeField] private float speedBuffDelay = 2f;
    [Space]
    [SerializeField] private float Radius = 1.5f;
    [SerializeField] private GameObject aura;
    [Space]
    [SerializeField] private LayerMask UnitLM; 

    private void Awake()
    {
        aura.GetComponent<Transform>().localScale = new Vector3(Radius * 2, Radius * 2, 1f);
        Invoke(nameof(StartSpeedBoost), speedBuffDelay);
    }

    IEnumerator SpeedBust()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Radius, Vector2.zero, 0, UnitLM);

        foreach (var hit in hits)
            hit.collider.GetComponent<UnitBase>().Tags.AddTemporarTag(UnitStatus.SpedUp, speedBuffDuration, false);

        Invoke(nameof(StartSpeedBoost), speedBuffDelay);
        
        yield break;
    }

    private void StartSpeedBoost()
        => StartCoroutine(SpeedBust());
}
