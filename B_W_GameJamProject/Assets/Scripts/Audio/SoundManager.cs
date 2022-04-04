using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{

    public enum Sound
    {
        Plyr_W_Move,
        Plyr_W_Jump,
        Plyr_W_Land,
        Plyr_B_Move,
        Plyr_B_Jump,
        Plyr_B_Land,
        Plyr_Coll_Switch,
        Itm_Pu_Generic,
        Obj_Map_Rotate,
    }

    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;




    public static void Intialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.Plyr_B_Move] = .1f;
        soundTimerDictionary[Sound.Plyr_W_Move] = .1f;
    }



    public static void PlaySound2D(Sound sound, Vector2 position) //play sound at position
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound2D");
            soundGameObject.layer = 1;
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }



    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }


    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            case Sound.Plyr_W_Move: //call only when you want based on time interval. so it doesn't play always
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .1f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
        
            case Sound.Plyr_B_Move: //call only when you want based on time interval. so it doesn't play always
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .05f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
   
            default:
                return true;
        }
    }




    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (AudioAssets.SoundAudioClip soundAudioClip in AudioAssets.i.soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }



}
