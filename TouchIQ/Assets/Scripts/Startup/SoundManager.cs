using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class SoundManager : MonoBehaviour
{
    private static SoundManager SM;

    public static SoundManager GetInstance()
    {
        if (SM == null)
        {
            SM = new GameObject("SoundManager").AddComponent<SoundManager>();
        }
        return SM;
    }

    public void Initialize()
    {

    }

    //Used to play single sound clips.
    public void PlaySingle(AudioClip clip)
    {
        StartCoroutine(coPlaySingle(clip));
    }

    public IEnumerator coPlaySingle(AudioClip clip)
    {
        AudioSource efxSource = gameObject.AddComponent<AudioSource>();

        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();

        while (efxSource.isPlaying)
        {
            yield return null;
        }

        Destroy(efxSource);
    }
}
