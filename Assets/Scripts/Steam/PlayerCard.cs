using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : NetworkBehaviour
{
    [SerializeField] private RawImage playerCardImage;
    [SerializeField] private Text playerName;
    [SerializeField] private Text playerStatus;
    [SerializeField] private int playerLevel;

    public void SetInfo(Texture cardImage, string playerName, string playerStatus, int playerLevel)
    {
        playerCardImage.texture = cardImage;
        this.playerName.text = playerName;
        this.playerStatus.text = playerStatus;
        this.playerLevel = playerLevel;
    }
}