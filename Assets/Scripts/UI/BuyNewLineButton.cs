using UnityEngine;
using UnityEngine.UI;

public class BuyNewLineButton : MonoBehaviour
{
    [SerializeField] private GameObject line;
    [SerializeField] private int price;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyLine);
    }

    private void BuyLine()
    {
        if (!BuyMenu.Instance.BuyNewUnitLine(price))
            return;

        line.SetActive(true);
        Destroy(gameObject);
    }
}
