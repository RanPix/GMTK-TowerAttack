using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenuBuilder : MonoBehaviour
{
    [SerializeField] private List<MapInfo> mapInfo;
    [SerializeField] private GameObject pickMapButton;

    [SerializeField] private SceneAsset scene;

    private void Start()
    {
        for (int i = 0; i < mapInfo.Count; i++)
        {
            GameObject newButton = Instantiate(pickMapButton, transform);
            GameObject currentMap = mapInfo[i].Map;
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            { 
                PickedMap.Map = currentMap;
                SceneManager.LoadScene(scene.name);
            });
            newButton.GetComponent<Image>().sprite = mapInfo[i].MapPreview;
            newButton.GetComponentInChildren<TMP_Text>().text = mapInfo[i].MapName + " | Difficulty: " + mapInfo[i].MapDifficulty;
        }
    }
}
