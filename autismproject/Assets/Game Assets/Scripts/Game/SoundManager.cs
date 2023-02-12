using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [BoxGroup("Music")] public AudioClip[] musicClip;
    [BoxGroup("Music")][SerializeField] AudioSource musicSource;
    [BoxGroup("Music")] public bool playOnStart = true;
    [BoxGroup("Music")][ShowIf("PlayOnStart")][MinValue(0)] public int startingIndex;

    void Start()
    {
        if(playOnStart)
            PlayMusic(startingIndex);
    }
    
    bool PlayOnStart() => playOnStart;
    public void PlayMusic(int index)
    {
        if(index >= musicClip.Length)
        {
            Debug.LogWarning("Index: " + index + " doesn't exist in music clips!");
            return;
        }
        musicSource.clip = musicClip[index];
        musicSource.Play();        
    }
    public void StopMusic()
    {
        musicSource.Stop();        
    }
}
