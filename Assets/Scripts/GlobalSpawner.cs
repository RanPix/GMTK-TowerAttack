using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> gameObjects;

    private void Start()
    {
        Instantiate(PickedMap.Map, transform.position, Quaternion.identity);

        foreach (GameObject gameObjectToSpawn in gameObjects)
            Instantiate(gameObjectToSpawn);
    }
}
