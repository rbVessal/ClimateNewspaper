using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool isDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Interfaces for pointer
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnPointerDown");
        }
    }

    // Interfaces for dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnBeginDrag");
        }

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnDrag");
        }

        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnEndDrag");
        }

        canvasGroup.blocksRaycasts = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
