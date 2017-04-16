using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class PictureSelector : MonoBehaviour {

    ScrollRect sv;
	// Use this for initialization
	void Start ()
    {
        PhotoController.GetInstance().OnActiveSetChanged += PhotoSetChanged;

        sv = this.GetComponent<ScrollRect>();
        PhotoSet set = PhotoController.GetInstance().ActiveSet;
        SetActivePhotos(set);

    }

    void SetActivePhotos(PhotoSet activeSet)
    {
        List<LayoutElement> les = sv.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach (LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        GameObject shareImagePrefab = Resources.Load<GameObject>("Prefabs/SeniorCall/ShareImage");
        

        foreach (Sprite image in activeSet.Photos)
        {
            GameObject go = GameObject.Instantiate<GameObject>(shareImagePrefab, sv.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            //Resources.Load<Sprite>("Textures/picture" + (i + 1).ToString());
            //go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/picture" + (i).ToString());
            go.GetComponent<Image>().sprite = image;
        }
    }

    private void PhotoSetChanged(PhotoSet activeSet)
    {
        SetActivePhotos(activeSet);
    }
}
