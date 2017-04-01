using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreate
{
    public static GameObject InstantiateRectTransformPrefab(GameObject uiPrefab, Transform parent)
    {
        GameObject prefab4 = Resources.Load<GameObject>("Prefabs/ContactsScreen/Popsicle");
        GameObject instantiated = GameObject.Instantiate<GameObject>(uiPrefab, uiPrefab.transform.position, uiPrefab.transform.rotation, parent);
        //Test4.GetComponent<RectTransform>().anchoredPosition = prefab4.GetComponent<RectTransform>().anchoredPosition;
        RectTransform prefabRect = prefab4.GetComponent<RectTransform>();
        RectTransform instantiatedRect = instantiated.GetComponent<RectTransform>();
        instantiatedRect.SetParent(parent);
        instantiatedRect.anchorMax = prefabRect.anchorMax;
        instantiatedRect.anchorMin = prefabRect.anchorMin;
        instantiatedRect.anchoredPosition = prefabRect.anchoredPosition;
        instantiatedRect.offsetMax = prefabRect.offsetMax;
        instantiatedRect.offsetMin = prefabRect.offsetMin;
        //instantiatedRect.offsetMax.Set(prefabRect.offsetMax.x, prefabRect.offsetMax.y);
        //instantiatedRect.offsetMin.Set(prefabRect.offsetMin.x, prefabRect.offsetMin.y);
        return instantiated;
    }
}
