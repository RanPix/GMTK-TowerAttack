using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    public Health HP { get; private set; }

    private void Awake()
    {
        HP = new(this);
        HP.Death += Death;
    }

    private void Death()
    {
        Destroy(this);
    }
}
