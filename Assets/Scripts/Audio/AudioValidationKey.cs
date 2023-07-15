
public struct AudioValidationKey
{
    public (AudioKind, AudioType, string) Key;

    public AudioValidationKey(AudioKind kind, AudioType type, string soundName)
    {
        Key = (kind, type, soundName);
    }
}
