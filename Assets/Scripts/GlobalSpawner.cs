using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects;
    void Start()
    {
        foreach (GameObject go in gameObjects)
        {
            Instantiate(go);
        }
    }
}
