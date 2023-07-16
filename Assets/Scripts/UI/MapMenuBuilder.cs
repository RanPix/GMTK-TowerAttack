using TMPro;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenuBuilder : MonoBehaviour
{
    [SerializeField] private MapsCollectionInfo maps;
    [SerializeField] private GameObject pickMapButton;

    [SerializeField] private SceneAsset scene;

    private void Start()
    {
        for (int i = 0; i < maps.Maps.Length; i++)
        {
            GameObject newButton = Instantiate(pickMapButton, transform);
            GameObject currentMap = maps.Maps[i];
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            { 
                PickedMap.Map = currentMap;
                SceneManager.LoadScene(scene.name);
            });
            newButton.GetComponent<Image>().sprite = maps.MapPreviews[i];
            newButton.GetComponentInChildren<TMP_Text>().text = maps.Names[i] + " Difficulty: " + maps.Difficulties[i];
        }
    }
}
