using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects;

    void Start()
    {
        foreach (GameObject gameObjectToSpawn in gameObjects)
            Instantiate(gameObjectToSpawn);
    }
}
