using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoDropItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDebug = false;
    public bool shouldSnap = true;
    
    // Drop interface
    public void OnDrop(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnDrop");
        }

        if (shouldSnap && eventData.pointerDrag != null)
        {

            GameObject draggedGameObject = eventData.pointerDrag;
            draggedGameObject.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>().transform);
            //draggedGameObject.transform.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            draggedGameObject.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;

            if (isDebug)
            {
                Debug.Log("Slotted");
            }
        }
    }
}
