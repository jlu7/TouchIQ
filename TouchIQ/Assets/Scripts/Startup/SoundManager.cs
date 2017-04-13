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
    private List<AudioSource> AudioSourceList;

    public static SoundManager GetInstance()
    {
        if (SM == null)
        {
            SM = new GameObject("SoundManager").AddComponent<SoundManager>();
            SM.AudioSourceList = new List<AudioSource>();
        }
        return SM;
    }

    public void Initialize()
    {
    }

    //Used to play single sound clips.
    public void PlaySingle(string path, bool loop = false)
    {
        StartCoroutine(coPlaySingle(Resources.Load<AudioClip>(path), loop));
    }

    public IEnumerator coPlaySingle(AudioClip clip, bool loop = false)
    {
        AudioSource efxSource = gameObject.AddComponent<AudioSource>();
        AudioSourceList.Add(efxSource);
        efxSource.loop = loop;

        //Set the clip of our efxSource audio source to the clip passed in as a parameter.
        efxSource.clip = clip;

        //Play the clip.
        efxSource.Play();

        while (efxSource.isPlaying)
        {
            yield return null;
        }

        AudioSourceList.Remove(efxSource);
        Destroy(efxSource);
    }

    public void StopAllLoopingSoundEffects()
    {
        foreach (AudioSource ass in AudioSourceList)
        {
            if (ass.loop == true)
            {
                ass.Stop();
            }
        }
    }
}
