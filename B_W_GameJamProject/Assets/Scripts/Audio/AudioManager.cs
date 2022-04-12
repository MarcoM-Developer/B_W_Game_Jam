using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }
                 
        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();

            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
		}
    }

    void Start()
	{
        Play("Theme"); 
	}

    public Sound GetSound(string name)
	{
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        return sound;
    }

    public void Play(string name)
	{
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Play();
	}

    public void Pause(string name)
	{
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Pause();
    }

    public void Stop(string name)
	{
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        sound.source.Stop();
    }
}
