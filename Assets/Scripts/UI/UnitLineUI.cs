using UnityEngine;

public class UnitLineUI : MonoBehaviour
{
    [SerializeField] private GameObject placementButton;
    [SerializeField] private int line;

    private void Start()
    {
        for (int i = 0; i < UnitSpawner.UNIT_TYPE_LINE_SIZE; i++)
        {
            GameObject newButton = Instantiate(placementButton, transform);
            newButton.GetComponent<UnitLineUIButton>().Setup(i, line);
        }
    }
}
