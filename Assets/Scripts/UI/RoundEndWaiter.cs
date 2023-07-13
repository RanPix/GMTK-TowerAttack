using UnityEngine;

public class RoundEndWaiter : MonoBehaviour
{
    [SerializeField] private GameObject objectToEnable;

    private void Start()
    {
        RoundManager.Instance.OnRoundEnd += RoundEnd;
    }

    private void RoundEnd()
    {
        if (objectToEnable != null)
            objectToEnable.SetActive(true);
    }
}
