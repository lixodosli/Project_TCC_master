using UnityEngine;

public enum AudioType
{
    BGM,
    BGA,
    SFX
}

public class AudioManager : AudioStateMachine
{
    public static AudioManager Instance;

    public AudioSource BGMSource => m_BGMSource;
    public AudioSource BGASource => m_BGASource;
    public AudioSource SFXSource => m_SFXSource;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(AudioClip clip, AudioType type, AudioConfigs config)
    {
        ChangeClip(type, clip, config);
    }
}

[System.Serializable]
public struct AudioConfigs
{
    [Range(0, 2)] public float Volume;
    public Vector2 PitchVariation;

    public static AudioConfigs Default()
    {
        AudioConfigs config = new AudioConfigs();
        config.Volume = 1;
        config.PitchVariation = Vector2.one;
        return config;
    }
}