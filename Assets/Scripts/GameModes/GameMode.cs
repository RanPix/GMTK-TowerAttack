using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameMode : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private TMP_Text nameText;
    
    public GameModeInfo Info { get; private set; }
    
    public void SetInfo(GameModeInfo info)
    {
        Info = info;
        
        image.texture = Info.ModeIcon;
        nameText.text = Info.ModeName;
    }

    public void Select()
    {
        if(!Info)
            return;
        
        print("select");
        
        GameModeInfoManager.Instance?.SetInfo(Info);
        
        if(GameModeHandler.Instance)
            GameModeHandler.Instance.chosenGameMode = Info;

        PickedMode.Info = Info;
    }
}