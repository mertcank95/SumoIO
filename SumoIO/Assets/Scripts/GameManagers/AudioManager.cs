using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //tüm oyun içindeki sesleri yönetmek için yazdýðým class 
    //singleton patterini uygulayýp bu nesneden sadece bir tane türetilmesini saðladým
    public static AudioManager instance;
    public AudioMixerGroup mixerGroup;
    public Sound[] sounds;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = mixerGroup;
        }
    }

    public void Play(string soundName)
    {
        Sound sound = FindSound(soundName);
        if (sound != null)
        {
            sound.source.Play();
        }
    }

    public void Stop(string soundName)
    {
        Sound sound = FindSound(soundName);
        if (sound != null)
        {
            sound.source.Stop();
        }
    }

    private Sound FindSound(string soundName)
    {
        return System.Array.Find(sounds, sound => sound.name == soundName);
    }
}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}

