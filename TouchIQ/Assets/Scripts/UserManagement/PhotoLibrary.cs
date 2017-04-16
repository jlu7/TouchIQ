using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoLibrary : MonoBehaviour {

    public PhotoSet[] PhotoSets;

	// Use this for initialization
	void Start () {
		
	}

}

[System.Serializable]
public class PhotoSet
{
    public string Name;
    public Texture2D[] Photos;

}
