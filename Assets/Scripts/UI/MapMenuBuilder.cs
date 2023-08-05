using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapMenuBuilder : MonoBehaviour
{
    [SerializeField] private List<MapInfo> mapInfo;
    [SerializeField] private GameObject pickMapButton;
    
    private void Start()
    {
        for (int i = 0; i < mapInfo.Count; i++)
        {
            GameObject newButton = Instantiate(pickMapButton, transform);
            GameObject currentMap = mapInfo[i].Map;
            newButton.GetComponent<Button>().onClick.AddListener(() => 
            { 
                PickedMode.Info.ModePrefab = currentMap;
                SceneManager.LoadScene("GameScene");
            });
            newButton.GetComponent<Image>().sprite = mapInfo[i].MapPreview;
            newButton.GetComponentInChildren<TMP_Text>().text = mapInfo[i].MapName + " | Difficulty: " + mapInfo[i].MapDifficulty;
        }
    }
}
