using System.Collections.Generic;
using UnityEngine;

public class Track : ScriptableObject
{
    [field : SerializeField] public List<Transform> MovementPoints { get; private set; }

    
}
