using UnityEngine;

namespace Towers
{
    public class SlowMotionField : MonoBehaviour
    {
        [SerializeField] private LayerMask unitLM;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == unitLM)
            {                
                other.GetComponent<UnitTags>()
                    .AddTag(UnitTypes.Slowness);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == unitLM)
            {
                other.GetComponent<UnitTags>()
                    .RemoveTag(UnitTypes.Slowness);
            }
        }
    }
}