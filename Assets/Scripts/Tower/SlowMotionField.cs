using Unity.VisualScripting;
using UnityEngine;

namespace Towers
{
    public class SlowMotionField : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            /*print(other.gameObject.layer + "     OBG");
            print( LayerMask.GetMask("Unit") + "    Layer");*/
            if (other.gameObject.layer == 3)
            {
                print("stooooooooooop");
                UnitTags unit = other.GetComponent<UnitTags>();
                unit.AddTag(UnitTypes.Slowness);
            }
        }
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == 3)
            {
                print("goooooooooo");
                UnitTags unit = other.GetComponent<UnitTags>();
                
                unit.RemoveTag(UnitTypes.Slowness);
            }
        }
    }
}