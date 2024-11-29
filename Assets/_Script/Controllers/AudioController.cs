using SerializableDictionary.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;

    public static AudioController Instance { get => instance; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private SerializableDictionary<AudioEnum, AudioClip> audios;
    [SerializeField] private SerializableDictionary<AudioEnum, AudioSource> audioSources;
    private List<AudioEnum> audioKeys = new();

    private void Start()
    {
        Init();

        Observer.AddObserver(ObserverConstants.StartGame, new((x) =>
        {
            PlayMusic(AudioEnum.bg_music);
            PlaySFX(AudioEnum.countdown);
        }));

        Observer.AddObserver(ObserverConstants.EndGame, new((x) =>
        {
            StopAllAudio();
            PlaySFX(AudioEnum.lose);
        }));
        
        Observer.AddObserver(ObserverConstants.WinGame, new((x) =>
        {
            StopAllAudio();
            PlaySFX(AudioEnum.win);
        }));
        Observer.AddObserver(ObserverConstants.UpdateCat, new((x) =>
        {
            PlaySFX(AudioEnum.cat_meow);
        }));
    }
    public void Init()
    {
        audioKeys.Clear();
        audioKeys = audios.Dictionary.Keys.ToList();
        foreach (AudioEnum key in audioKeys)
        {
            if (audioSources.ContainsKey(key)) break;
            AudioSource tmp = gameObject.AddComponent<AudioSource>();
            tmp.clip = audios.Dictionary[key];
            audioSources.Add(key, tmp);
        }
    }

    public void PlaySFX(AudioEnum audio)
    {
        audioSources.Dictionary[audio].loop = false;
        audioSources.Dictionary[audio].Play();
    }
    public void PlayMusic(AudioEnum audio)
    {
        audioSources.Dictionary[audio].Stop();
        audioSources.Dictionary[audio].loop = true;
        audioSources.Dictionary[audio].Play();
    }
    public void StopAudio(AudioEnum audio)
    {
        audioSources.Dictionary[audio].Stop();
    }

    public void PlayButtonSound()
    {
        PlaySFX(AudioEnum.click);
    }

    public void StopAllAudio()
    {
        foreach (AudioEnum key in audioKeys)
        {
            audioSources.Dictionary[key].Stop();

        }
    }
}
