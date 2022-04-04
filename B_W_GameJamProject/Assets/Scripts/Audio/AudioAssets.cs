
using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    private static AudioAssets _i;

    public static AudioAssets i
    {
        get
        {
            if(_i == null) _i = (Instantiate(Resources.Load("AudioAssets")) as GameObject).GetComponent<AudioAssets>();
            return _i;  
        }
    }

    private void Awake()
    {
        SoundManager.Intialize();
    }

    public SoundAudioClip[] soundAudioClipArray;

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
    }



}


