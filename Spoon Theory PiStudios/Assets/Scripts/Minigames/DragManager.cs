using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] 
    private Canvas canvas;

    Collider2D myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }
    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)canvas.transform, 
            pointerData.position, 
            canvas.worldCamera, 
            out position);

        transform.position = canvas.transform.TransformPoint(position);
    } 
    
    public void DeactivateCollider()
    {
        myCollider.enabled = false;
    }

    public void ActivateCollider()
    {
        myCollider.enabled = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DeactivateCollider();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ActivateCollider();
    }
}
