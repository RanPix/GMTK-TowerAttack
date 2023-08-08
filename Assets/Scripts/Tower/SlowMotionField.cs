using UnityEngine;
using Assets.Scripts.Unit.UnitTags;

namespace Towers
{
    public class SlowMotionField : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {                
                other.GetComponent<UnitTags>()
                    .AddTag(UnitStatus.Slowed);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                other.GetComponent<UnitTags>()
                    .RemoveTag(UnitStatus.Slowed);
            }
        }
    }
}