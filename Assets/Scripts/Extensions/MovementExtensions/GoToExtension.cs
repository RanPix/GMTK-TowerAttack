
using UnityEngine;

public static class GoToExtension
{
    public static void GoTo(this Transform transform, Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
