using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    public bool isDebug = false;
    public bool isDraggable = true;

    public delegate void OnEndDragDelegate();
    public OnEndDragDelegate onEndDragDelegate;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
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

        if (isDraggable)
        {
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnDrag");
        }

        if (isDraggable)
        {
            rectTransform.anchoredPosition += eventData.delta /  canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnEndDrag");
        }

        if (isDraggable)
        {
            canvasGroup.blocksRaycasts = true;

            // It's possible to get a null error if we don't check
            // to see if there are any subscribers to this delegate
            if (onEndDragDelegate != null)
            {
                onEndDragDelegate();
            }
        }
    }
}
