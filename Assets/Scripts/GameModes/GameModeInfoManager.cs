using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameModeInfoManager : MonoBehaviour
{
    private static GameModeInfoManager _Instance;
    public static GameModeInfoManager Instance
    {
        get => _Instance;
        private set
        {
            if (_Instance == null)
                _Instance = value;
        }
    }
    
    [SerializeField] private RawImage[] images;
    [SerializeField] private TMP_Text[] nameTexts;
    [SerializeField] private TMP_Text[] descriptionTexts;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        Instance = this;
    }
    public void SetInfo(GameModeInfo info)
    {
        foreach (var image in images)
        {
            image.texture = info.ModeIcon;
        }

        foreach (var text in nameTexts)
        {
            text.text = info.ModeName;
        }

        foreach (var text in descriptionTexts)
        {
            text.text = info.Description;
        }
    }
}
