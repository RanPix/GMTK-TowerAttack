using UnityEngine;

public class RoundEndWaiter : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;

    private void Start()
    {
        RoundManager.OnRoundEnd += RoundEnd;
    }

    private void RoundEnd()
    {
        objectToEnable.SetActive(true);
    }
}
