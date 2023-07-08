using UnityEngine;

public class UnitBase : MonoBehaviour
{
    [field: SerializeField] public UnitTemplate unitData { get; private set; }
    public Health HP { get; private set; }

    private void Awake()
    {
        HP = new(this);
        HP.Death += () 
            => Destroy(this);
    }

    private void Update()
    {
        var thing = Physics2D.OverlapCircle(new Vector2(1, 1), 2);
    }
}
