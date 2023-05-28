using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStateMachine : MonoBehaviour
{
    // -- Configurations
    [SerializeField] private float m_AudioClipChangeSpeed;

    // -- AudioSources
    [SerializeField] protected AudioSource m_BGMSource;
    [SerializeField] protected AudioSource m_BGASource;
    [SerializeField] protected AudioSource m_SFXSource;

    // -- Current Clip
    private AudioClip _CurrentBGMClip;
    private AudioClip _CurrentBGAClip;
    private AudioClip _CurrentSFXClip;
    public AudioClip CurrentBGMClip => _CurrentBGMClip;
    public AudioClip CurrentBGAClip => _CurrentBGAClip;
    public AudioClip CurrentSFXClip => _CurrentSFXClip;

    // -- Counter Infos
    protected ChangeInfo _BGMInfos;
    protected ChangeInfo _BGAInfos;
    protected ChangeInfo _SFXInfos;

    private void Start()
    {
        _BGMInfos = new ChangeInfo(m_BGMSource);
        _BGAInfos = new ChangeInfo(m_BGASource);
        _SFXInfos = new ChangeInfo(m_SFXSource);
    }

    private void Update()
    {
        UpdateChange(_BGMInfos);
        UpdateChange(_BGAInfos);
        UpdateChange(_SFXInfos);
    }

    protected void UpdateChange(ChangeInfo info)
    {
        if (!info.Change)
            return;

        info.Source.volume = info.Counter;

        if (!info.Source.isPlaying)
        {
            info.Source.clip = info.NextClip;
            info.Source.Play();
        }

        if (info.Growing)
        {
            info.Counter += Time.deltaTime * m_AudioClipChangeSpeed;

            if (info.Counter >= info.MaxVolume)
            {
                info.Counter = info.MaxVolume;
                info.Change = false;
            }
        }
        else
        {
            info.Counter -= Time.deltaTime * m_AudioClipChangeSpeed;

            if (info.Counter <= 0)
            {
                info.Counter = 0;
                info.Source.Stop();

                info.Source.clip = info.NextClip;
                info.Source.Play();

                info.Growing = true;
            }
        }

        info.Source.volume = info.Counter;
    }

    public void ChangeClip(AudioType type, AudioClip newClip, AudioConfigs configs)
    {
        switch (type)
        {
            case AudioType.BGM:
                if (newClip == m_BGMSource.clip)
                    return;

                StartChangeBGM(newClip, configs);
                break;
            case AudioType.BGA:
                if (newClip == m_BGASource.clip)
                    return;

                StartChangeBGA(newClip, configs);
                break;
            case AudioType.SFX:
                m_SFXSource.PlayOneShot(newClip);
                //StartChangeSFX(newClip, configs);
                break;
        }
    }

    private void StartChangeBGM(AudioClip newClip, AudioConfigs configs)
    {
        // -- Definir qual o max volume e pitch baseado nas configs
        _BGMInfos.MaxVolume = configs.Volume;
        _BGMInfos.Source.pitch = Random.Range(configs.PitchVariation.x, configs.PitchVariation.y);

        // -- Defino qual o proximo clip que vou tocar, depois do grow
        _BGMInfos.NextClip = newClip;

        // -- Digo se esta fazendo GrowIn ou GrowOut
        _BGMInfos.Growing = _BGMInfos.Source.isPlaying ? false : true;

        // -- Inicio o contador a partir do Grow
        _BGMInfos.Counter = _BGMInfos.Growing ? 0 : _BGMInfos.MaxVolume;
        _BGMInfos.Source.volume = _BGMInfos.Counter;

        // -- Inicio a mudanca
        _BGMInfos.Change = true;
    }

    private void StartChangeBGA(AudioClip newClip, AudioConfigs configs)
    {
        // -- Definir qual o max volume e pitch baseado nas configs
        _BGAInfos.MaxVolume = configs.Volume;
        _BGAInfos.Source.pitch = Random.Range(configs.PitchVariation.x, configs.PitchVariation.y);

        // -- Defino qual o proximo clip que vou tocar, depois do grow
        _BGAInfos.NextClip = newClip;

        // -- Digo se esta fazendo GrowIn ou GrowOut
        _BGAInfos.Growing = _BGAInfos.Source.isPlaying ? false : true;

        // -- Inicio o contador a partir do Grow
        _BGAInfos.Counter = _BGAInfos.Growing ? 0 : _BGAInfos.MaxVolume;
        _BGAInfos.Source.volume = _BGAInfos.Counter;

        // -- Inicio a mudanca
        _BGAInfos.Change = true;
    }

    private void StartChangeSFX(AudioClip newClip, AudioConfigs configs)
    {
        // -- Definir qual o max volume e pitch baseado nas configs
        _SFXInfos.MaxVolume = configs.Volume;
        _SFXInfos.Source.pitch = Random.Range(configs.PitchVariation.x, configs.PitchVariation.y);

        // -- Defino qual o proximo clip que vou tocar, depois do grow
        _SFXInfos.NextClip = newClip;

        // -- Digo se esta fazendo GrowIn ou GrowOut
        _SFXInfos.Growing = _SFXInfos.Source.isPlaying ? false : true;

        // -- Inicio o contador a partir do Grow
        _SFXInfos.Counter = _SFXInfos.Growing ? 0 : _SFXInfos.MaxVolume;
        _SFXInfos.Source.volume = _SFXInfos.Counter;

        // -- Inicio a mudanca
        _SFXInfos.Change = true;
    }
}

[System.Serializable]
public class ChangeInfo
{
    public bool Change;
    public bool Growing;
    public float Counter;
    public float MaxVolume;
    public AudioSource Source;
    public AudioClip NextClip;

    public ChangeInfo(AudioSource source)
    {
        Source = source;
    }
}