using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private Transform transform;
    void Update()
    {
        if(!NetworkClient.localPlayer)
            return;
        
        if (Input.GetKeyDown(KeyCode.W))
            transform.position = new Vector3(transform.position.x, transform.position.y+4, transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.S))
            transform.position = new Vector3(transform.position.x, transform.position.y-4, transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.A))
            transform.position = new Vector3(transform.position.x-4, transform.position.y, transform.position.z);
        
        if (Input.GetKeyDown(KeyCode.D))
            transform.position = new Vector3(transform.position.x+4, transform.position.y, transform.position.z);
    }
}
