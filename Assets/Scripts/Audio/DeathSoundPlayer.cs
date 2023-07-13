using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSoundPlayer : MonoBehaviour
{
    public static DeathSoundPlayer Instance { get; private set; }

    private AudioSource deathAudioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        deathAudioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void PlayDeathSound()
        => deathAudioSource.Play();
}
