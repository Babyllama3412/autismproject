using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Band : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] instruments;

    public void PlayInstrument(int index)
    {
        audioSource.PlayOneShot(instruments[index]);
    }
}
