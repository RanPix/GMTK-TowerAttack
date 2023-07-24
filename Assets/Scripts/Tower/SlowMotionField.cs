using UnityEngine;

namespace Towers
{
    public class SlowMotionField : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {                
                other.GetComponent<UnitTags>()
                    .AddTag(UnitTypes.Slowness);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                other.GetComponent<UnitTags>()
                    .RemoveTag(UnitTypes.Slowness);
            }
        }
    }
}