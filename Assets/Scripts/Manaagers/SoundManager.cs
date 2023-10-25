using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

[Serializable]
public class SerializeDictionary : SerializedDictionary<string,AudioClip>{}

public class SoundManager : SingleTon<SoundManager>
{
    private readonly string _audioPath = "Audio/";
    
    [Header("Sound")]
    [SerializeField]
    private SerializeDictionary audioClips = new();

    private GameObject _goBGM;
    private List<GameObject> _effectSounds;
    private GameObject _effectSoundBox;
    private AudioSource _bgAudioSource;
    
    GameObject GOBgSound
    {
        get
        {
            if (_goBGM == null)
            {
                _goBGM = new GameObject("BGM");
                _goBGM.AddComponent<AudioSource>();
            }

            return _goBGM;
        }
    }
    
    AudioSource BgAudioSource
    {
        get
        {
            if (_bgAudioSource == null)
            {
                _bgAudioSource = GOBgSound.GetComponent<AudioSource>();
            }

            return _bgAudioSource;
        }
    }

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        _effectSoundBox = new GameObject("EffectSoundBox");
    }

    AudioClip GetAuidoClip(SoundType soundType)
    {
        AudioClip audioClip;
        try
        {
            audioClip = Resources.Load<AudioClip>($"{_audioPath}{Convert.ToString(soundType)}");
            audioClips.Add(soundType.ToString(),audioClip);
        }
        catch (Exception e)
        {
            Console.WriteLine($"오디오 클립을 가져 오는중 오류 발생 {e}");
            throw;
        }
        

        return audioClip;
    }

    public void PlayBGM(SoundType soundType,float bgVolume = 1f)
    {
        string clipName = soundType.ToString();
        AudioClip clip;
        if (!audioClips.TryGetValue(clipName, out clip))
        {
            clip = GetAuidoClip(soundType);
        };

        BgAudioSource.clip = clip;
        BgAudioSource.loop = true;
        BgAudioSource.volume = bgVolume;
        BgAudioSource.dopplerLevel = 0;
        BgAudioSource.reverbZoneMix = 0;
        BgAudioSource.Play();
    }

    public void PlayUISound(SoundType soundType)
    {
        string clipName = soundType.ToString();
        AudioClip clip;
        if (!audioClips.TryGetValue(clipName, out clip))
        {
            clip = GetAuidoClip(soundType);
        };
        AudioSource audioSource;

        GameObject go = new GameObject(clipName);
        audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(go,audioSource.clip.length);
    }

    public void PlayEffect(SoundType soundType,float volume = 1f)
    {
        string clipName = soundType.ToString();
        Transform clipBoxTransform = _effectSoundBox.transform.Find(clipName);
        AudioClip clip;
        if (!audioClips.TryGetValue(clipName, out clip))
        {
            clip = GetAuidoClip(soundType);
        };
        AudioSource audioSource;

        if (clipBoxTransform == null)
        {
            var clipSoundBox = new GameObject(clipName);
            clipSoundBox.name = clipName;
            clipBoxTransform = clipSoundBox.transform;
            clipSoundBox.transform.parent = _effectSoundBox.transform;
        }
        
        for (int i = 0; i < clipBoxTransform.childCount; i++)
        {
            var sound = clipBoxTransform.GetChild(i).gameObject;
            if(!sound.activeInHierarchy)
            {
                sound.SetActive(true);
                StartCoroutine(EffectSoundClose(sound.GetComponent<AudioSource>()));
                return;
            }
        }
        var goSound = new GameObject(clipName);
        audioSource = goSound.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        goSound.transform.parent = clipBoxTransform;
        StartCoroutine(EffectSoundClose(audioSource));
    }

    IEnumerator EffectSoundClose(AudioSource audioSource)
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audioSource.gameObject.SetActive(false);
    }
}

public enum SoundType
{
    BGM,
}
