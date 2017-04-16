using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public delegate void ActiveSetChanged(PhotoSet activeSet);
    public event ActiveSetChanged OnActiveSetChanged;

    public PhotoSet ActiveSet = null;

    private PhotoLibrary photoLibrary;

    public void Initialize()
    {
        SpeechController.GetInstance().OnContextSpeechDetected += OnPhotoContextWord;
        photoLibrary = Instantiate<GameObject>(Resources.Load<GameObject>("prefabs/PhotoLibrary")).GetComponent<PhotoLibrary>();
        ActiveSet = photoLibrary.PhotoSets[0];
    }

    public Sprite GetPhoto(string setName, string photoName)
    {
        PhotoSet set = photoLibrary.PhotoSets.FirstOrDefault(x => x.Name.ToLower() == setName.ToLower());
        if(null != set)
        {
            return set.Photos.FirstOrDefault(y => y.name.ToLower() == photoName.ToLower());
        }

        return null;
    }

    private void OnPhotoContextWord(List<string> contextWords)
    {
        bool setChanged = false;
        foreach(PhotoSet set in photoLibrary.PhotoSets)
        {
            if(set.Name.ToLower() == contextWords[0])
            {
                ActiveSet = set;
                setChanged = true;
                break;
            }
        }
        if(null != OnActiveSetChanged && setChanged)
        {
            OnActiveSetChanged(ActiveSet);
        }
    }
}
