using UnityEngine;
using UnityEngine.UI;

public class NextRoundButton : MonoBehaviour
{
    [SerializeField] private Button button;

    private void Start()
    {
        button.onClick.AddListener(UnitSpawner.Instance.StartNextRound);
    }
}
