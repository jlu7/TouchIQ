using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoController : MonoBehaviour {

    private static PhotoController photoController;

    public static PhotoController GetInstance()
    {
        if (photoController == null)
        {
            photoController = new GameObject("PhotoController").AddComponent<PhotoController>();
        }
        return photoController;
    }

    public void Initialize()
    {
        SpeechController.GetInstance().OnContextSpeechDetected += OnPhotoContextWord;
    }

    private void OnPhotoContextWord(List<string> contextWords)
    {
        throw new NotImplementedException();
    }
}
