using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class PictureSelector : MonoBehaviour {

    ScrollRect sv;
	// Use this for initialization
	void Start ()
    {
        sv = this.GetComponent<ScrollRect>();
        List<LayoutElement> les = sv.content.GetComponent<VerticalLayoutGroup>().GetComponentsInChildren<LayoutElement>().ToList();
        foreach(LayoutElement le in les)
        {
            Destroy(le.gameObject);
        }
        GameObject shareImagePrefab = Resources.Load<GameObject>("Prefabs/SeniorCall/ShareImage");
        for(int i = 0; i < 7; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(shareImagePrefab, sv.content.transform);
            go.GetComponent<RectTransform>().localScale = Vector3.one;
            //Resources.Load<Sprite>("Textures/picture" + (i + 1).ToString());
            go.GetComponent<Image>().sprite = Resources.Load<Sprite>("Textures/picture" + (i + 1).ToString());
        }
        
	}

}
