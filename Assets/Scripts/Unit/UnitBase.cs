using UnityEngine;

public class UnitBase : MonoBehaviour
{
    private void Update()
    {
        var thing = Physics2D.OverlapCircle(new Vector2(1, 1), 2);
    }
}
