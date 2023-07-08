using UnityEngine;
using UnityEngine.UI;

public class UnitLineUI : MonoBehaviour
{
    [SerializeField] private GameObject placementButton;
    [SerializeField] private int line;

    private void Start()
    {
        for (int i = 0; i < 30; i++)
        {
            GameObject newButton = Instantiate(placementButton, transform);
            newButton.GetComponent<UnitLineUIButton>().Setup(i, line);
        }
    }
}
