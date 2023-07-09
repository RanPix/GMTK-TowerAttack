using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class DeathSoundPlayer : MonoBehaviour
{
    public static DeathSoundPlayer instance;

    private AudioSource deathAudioSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        deathAudioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
        => instance = null;

    public void PlayDeathSound()
        => deathAudioSource.Play();
}
