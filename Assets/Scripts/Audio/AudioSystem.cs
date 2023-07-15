using System;
using System.Collections.Generic;
using UnityEngine;

public enum AudioKind
{ 
    Towers,
    Creeps,
    Music,
}
public enum AudioType
{ 
    CannonTower,
    CrossbowTower,
    FireTower,
    IceTower,
    TeslaTower,
    CreepAttack,
    CreepDeath,
    Bullet,
    Music,
}

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem instance { get; private set; }

    [SerializeField] private List<SoundScriptableObject> soundsScriptableObjects;

    private Dictionary<AudioValidationKey, List<AudioClip>> audioClips = new();

    private Dictionary<AudioKind, AudioSource> audioSources = new();

    #region Initialization

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.LogWarning("Audio System instance already exists!");
    }

    private void Start()
    {
        FetchAudioSources();

        CreateSounds();
    }

    private void FetchAudioSources()
    {
        var sources = GetComponentsInChildren<AudioSource>();

        foreach(var source in sources)
        {
            AudioKind kind;

            Enum.TryParse(source.gameObject.name, out kind);
            
            if(!audioSources.ContainsKey(kind))
                audioSources.Add(kind, source);
        }
    }

    private void CreateSounds()
    {
        foreach(var scriptableObject in soundsScriptableObjects)
        {
            var key = new AudioValidationKey(scriptableObject.Kind, scriptableObject.Type, scriptableObject.SoundName);

            audioClips.Add(key, scriptableObject.SoundList);
        }
    }

    #endregion

    public void PlaySound(AudioKind sourceKind, AudioValidationKey validationKey, bool isRandom)
    {
        if(!audioSources.ContainsKey(sourceKind))
        {
            Debug.LogWarning("No suitable audio source!");
            return;
        }

        if(!audioClips.ContainsKey(validationKey))
        {
            Debug.LogWarning("No suitable sound(s)!");
            return;
        }

        var source = audioSources[sourceKind];

        var sound = audioClips[validationKey];

        int chosenSoundIndex = 0;

        if (isRandom)
            chosenSoundIndex = UnityEngine.Random.Range(0, sound.Count);

        source.clip = sound[chosenSoundIndex];

        source.Play();
    }
}
