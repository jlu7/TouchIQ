using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreate
{
    public static GameObject InstantiateRectTransformPrefab(GameObject uiPrefab, RectTransform parent)
    {
        GameObject instantiated = GameObject.Instantiate<GameObject>(uiPrefab, uiPrefab.transform.position, uiPrefab.transform.rotation, parent);
        RectTransform instantiatedRect = instantiated.GetComponent<RectTransform>();
        instantiatedRect.SetParent(parent, false);
        instantiatedRect.anchoredPosition = uiPrefab.GetComponent<RectTransform>().anchoredPosition;

        return instantiated;
    }
}
