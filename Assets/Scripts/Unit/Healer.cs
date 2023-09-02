using System.Collections;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] private float healAmount = 1.5f;
    [SerializeField] private float healDelay = 3f;
    [Space]
    [SerializeField] private int healTicks = 2;
    [SerializeField] private float healTicksDelay = 0.5f;
    [Space]
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private GameObject aura;
    [Space]
    [SerializeField] private LayerMask unitLM;

    private void Awake()
    {
        aura.GetComponent<Transform>().localScale = new Vector3(radius * 2, radius * 2, 1f);
        Invoke(nameof(StartHeal), healDelay);
    }

    private IEnumerator Heal()
    {
        for (int i = 0; i < healTicks; i++)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, radius, Vector2.zero, 0, unitLM);
            
            foreach (var hit in hits)
                hit.collider.GetComponent<UnitBase>().HP.Heal(healAmount);

            yield return new WaitForSeconds(healTicksDelay);
        }

        Invoke(nameof(StartHeal), healDelay);
        
        yield break;
    }

    private void StartHeal()
        => StartCoroutine(Heal());
}
