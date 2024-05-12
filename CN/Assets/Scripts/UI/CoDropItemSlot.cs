using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoDropItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDebug = false;
    public bool shouldSnap = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Drop interface
    public void OnDrop(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnDrop");
        }

        if (shouldSnap && eventData.pointerDrag != null)
        { 
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
