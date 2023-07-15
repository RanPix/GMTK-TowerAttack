using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/Audio/SoundScriptableObject")]
public class SoundScriptableObject : ScriptableObject
{
    public AudioKind Kind;
    public AudioType Type;
    [Space]
    public string SoundName;
    [Space]
    public List<AudioClip> SoundList;
}
