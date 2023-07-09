using UnityEngine;

public class DeathEffectScript : MonoBehaviour
{
    private void Start()
    {
        GetComponent<ParticleSystem>().Play();

        Destroy(gameObject, 1f);
    }
}
