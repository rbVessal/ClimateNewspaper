using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;

[System.Serializable]
public class DroppedItemSlotEvent : UnityEvent<GameObject, GameObject> { }

[System.Serializable]
public class RemovedItemSlotEvent : UnityEvent<GameObject, GameObject> { }

public class CoDropItemSlot : MonoBehaviour, IDropHandler
{
    public bool isDebug = false;
    public bool shouldSnap = true;
    public bool isOccupied = false;

    public GameObject occupiedGameObject;

    public DroppedItemSlotEvent onDroppedItemEvent;
    public RemovedItemSlotEvent onRemovedItemEvent;


    private void Update()
    {
        if (isDebug)
        {
            if (occupiedGameObject != null)
            {
                Vector3[] objectCorners = new Vector3[4];
                occupiedGameObject.GetComponent<RectTransform>().GetWorldCorners(objectCorners);
                Debug.DrawLine(objectCorners[0] /* bottomLeft*/, objectCorners[1] /*topLeft*/, Color.red); // left
                Debug.DrawLine(objectCorners[1] /*topLeft*/, objectCorners[2] /*topRight*/, Color.red); // top
                Debug.DrawLine(objectCorners[2] /*topRight*/, objectCorners[3] /*bottomRight*/, Color.red); // right
                Debug.DrawLine(objectCorners[3] /*bottomRight*/, objectCorners[0] /*bottomLeft*/, Color.red); // bottom
            }

            Vector3[] corners = new Vector3[4];
            this.gameObject.GetComponent<RectTransform>().GetWorldCorners(corners);
            Debug.DrawLine(corners[0], corners[1], Color.green); // left
            Debug.DrawLine(corners[1], corners[2], Color.green); // top
            Debug.DrawLine(corners[2], corners[3], Color.green); // right
            Debug.DrawLine(corners[3], corners[0], Color.green); // bottom
        }
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
            GameObject droppedGameObject = eventData.pointerDrag;
            droppedGameObject.transform.GetComponent<RectTransform>().position = this.gameObject.GetComponent<RectTransform>().position;

            if (droppedGameObject != occupiedGameObject)
            {
                isOccupied = true;
                occupiedGameObject = droppedGameObject;
                CoDragDrop dragDrop = occupiedGameObject.GetComponent<CoDragDrop>();
                if (dragDrop != null)
                {
                    dragDrop.onEndDragDelegate += OnOccupiedObjectEndDrag;
                }

                onDroppedItemEvent.Invoke(occupiedGameObject, this.gameObject);
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
                Vector3[] objectCorners = new Vector3[4];
                draggedObjectRectTransform.GetWorldCorners(objectCorners);
                
                // Note corner 0 is bottom left and corner 2 is top right
                Rect draggedObjectRect = new Rect(objectCorners[0].x, 
                                                objectCorners[0].y, 
                                                objectCorners[2].x - objectCorners[0].x,
                                                objectCorners[2].y - objectCorners[0].y);

                Vector3[] slotCorners = new Vector3[4];
                RectTransform slotRectTransform = this.gameObject.GetComponent<RectTransform>();
                slotRectTransform.GetWorldCorners(slotCorners);

                Rect slotRect = new Rect(slotCorners[0].x,
                                        slotCorners[0].y,
                                        slotCorners[2].x - slotCorners[0].x,
                                        slotCorners[2].y - slotCorners[0].y);

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

                    onRemovedItemEvent.Invoke(occupiedGameObject, this.gameObject);

                    occupiedGameObject = null;
                }
            }
        }
    }

    public void ClearItemInSlot()
    {
        if (occupiedGameObject != null)
        {
            CoDragDrop coDragDrop = occupiedGameObject.GetComponent<CoDragDrop>();
            if (coDragDrop != null)
            {
                coDragDrop.onEndDragDelegate -= OnOccupiedObjectEndDrag;
            }

            // Reset the position otherwise it will look like it's still in the slot
            occupiedGameObject.transform.localPosition = Vector3.zero;
        }

        isOccupied = false;
    }

    public GameObject GetSlottedArticle()
    {
        if (isOccupied)
        {
            return occupiedGameObject;
        }
        else
        {
            return null;
        }
    }
}
