using UnityEngine;

public class GameModeMenuBuilder : MonoBehaviour
{
    [SerializeField] private GameModeInfo[] gameModes;
    [Space] 
    [SerializeField] private GameObject gameModesPanel;
    [SerializeField] private GameMode gameModeGameObject;

    private void Start()
    {
        foreach (var info in gameModes)
        {
            Instantiate(gameModeGameObject, gameModesPanel.transform).SetInfo(info);
        }
    }
}
