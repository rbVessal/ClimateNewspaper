using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[System.Serializable]
public class DroppedItemSlotEvent : UnityEvent<GameObject> { }

public class CoDropItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDebug = false;
    public bool shouldSnap = true;
    public bool isOccupied = false;

    private GameObject occupiedGameObject;

    public DroppedItemSlotEvent onDroppedItemEvent;
    
    // Drop interface
    public void OnDrop(PointerEventData eventData)
    {
        if (isDebug)
        {
            Debug.Log("OnDrop");
        }

        if (shouldSnap && eventData.pointerDrag != null)
        {
            GameObject droppedGameObject = eventData.pointerDrag;
            droppedGameObject.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>().transform);
            //draggedGameObject.transform.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            droppedGameObject.transform.GetComponent<RectTransform>().localPosition = Vector3.zero;

            if (droppedGameObject != occupiedGameObject)
            {
                isOccupied = true;
                occupiedGameObject = droppedGameObject;
                CoDragDrop dragDrop = occupiedGameObject.GetComponent<CoDragDrop>();
                if (dragDrop != null)
                {
                    dragDrop.onEndDragDelegate += OnOccupiedObjectEndDrag;
                }

                onDroppedItemEvent.Invoke(occupiedGameObject);
            }

            if (isDebug)
            {
                Debug.Log("Slotted");
            }
        }
    }

    private Rect GetWorldSpaceRect(RectTransform rectTransform)
    {
        Rect rect;
        rect = rectTransform.rect;
        rect.center = rectTransform.TransformPoint(rect.center);
        rect.size = rectTransform.TransformVector(rect.size);

        return rect;
    }

    // Detect when it has been unslotted
    public void OnOccupiedObjectEndDrag()
    {
        if (isDebug)
        {
            Debug.Log("CoDropItem - OnEndDrag");
        }

        if (occupiedGameObject != null)
        {
            Debug.Log("CoDropItem - occupied game object");

            RectTransform draggedObjectRectTransform = occupiedGameObject.GetComponent<RectTransform>();
            if (draggedObjectRectTransform)
            {
                Debug.Log("CoDropItem - occupied game object rect transform");
                Rect draggedObjectRect = GetWorldSpaceRect(draggedObjectRectTransform);
                Rect slotRect = GetWorldSpaceRect(GetComponent<RectTransform>());
                if (!draggedObjectRect.Overlaps(slotRect))
                {
                    if (isDebug)
                    {
                        Debug.Log("CoDropItem - On unslotted");
                    }

                    isOccupied = false;
                    CoDragDrop coDragDrop = occupiedGameObject.GetComponent<CoDragDrop>();
                    if (coDragDrop != null)
                    { 
                        coDragDrop.onEndDragDelegate -= OnOccupiedObjectEndDrag;
                    }
                    occupiedGameObject = null;
                }
            }
        }
    }
}
