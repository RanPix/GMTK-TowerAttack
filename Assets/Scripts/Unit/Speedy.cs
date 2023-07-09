using System.Collections;
using UnityEngine;

public class Speedy : MonoBehaviour
{
    [SerializeField] private float speedBuffDuration = 1.5f;
    [SerializeField] private float speedBuffDelay = 2f;
    [SerializeField] private float Radius = 1.5f;

    private void Awake()
    {
        GetComponentInChildren<Transform>().localScale = new Vector3(Radius * 2, Radius * 2, 1f);
        Invoke("StartSpeedBoost", speedBuffDelay);
    }

    IEnumerator SpeedBust()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Radius, Vector2.zero, 0, LayerMask.GetMask("Unit"));

        foreach (var hit in hits)
        {
            hit.collider.GetComponent<UnitTags>().AddTemporarTag(UnitTypes.SpedUp, speedBuffDuration);
        }

        Invoke("StartSpeedBoost", speedBuffDelay);
        
        yield break;
    }

    private void StartSpeedBoost()
        => StartCoroutine(SpeedBust());
}
