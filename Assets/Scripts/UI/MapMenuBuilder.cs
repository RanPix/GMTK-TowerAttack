using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenuBuilder : MonoBehaviour
{
    [SerializeField] private MapsCollectionInfo maps;
    [SerializeField] private GameObject pickMapButton;

    private void Start()
    {
        for (int i = 0; i < maps.maps.Length; i++)
        {
            GameObject newButton = Instantiate(pickMapButton, transform);
            GameObject currentMap = maps.maps[i];
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            { 
                PickedMap.map = currentMap;
                SceneManager.LoadScene("SampleScene");
            });
            newButton.GetComponent<Image>().sprite = maps.mapPreviews[i];
            newButton.GetComponentInChildren<TMP_Text>().text = maps.name[i] + " Difficulty: " + maps.difficulties[i];
        }
    }
}
