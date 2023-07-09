using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Healer : MonoBehaviour
{
    [SerializeField] private float healAmount = 1.5f;
    [SerializeField] private float healDelay = 3f;
    [Space]
    [SerializeField] private int healTicks = 2;
    [SerializeField] private float healTicksDelay = 0.5f;
    [Space]
    [SerializeField] private float Radius = 0.75f;

    private void Awake()
    {
        Invoke("StartHeal", healDelay);
        GetComponentInChildren<Transform>().localScale = new Vector3(Radius * 2, Radius * 2, 1f);
    }

    private IEnumerator Heal()
    {

        for (int i = 0; i < healTicks; i++)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, Radius, Vector2.zero, 0, LayerMask.GetMask("Unit"));
            
            foreach (var hit in hits)
            {
                hit.collider.GetComponent<UnitBase>().HP.Heal(healAmount);
            }

            yield return new WaitForSeconds(healTicksDelay);
        }

        Invoke("StartHeal", healDelay);
        
        yield break;
    }

    private void StartHeal()
        => StartCoroutine(Heal());
}
