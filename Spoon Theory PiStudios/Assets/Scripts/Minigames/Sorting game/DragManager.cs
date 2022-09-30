using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] 
    private Canvas canvas;

    Collider2D myCollider;
    Vector3 lastClickedPos;

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

        Vector3 clickedPos = canvas.transform.TransformPoint(position);

        float y;
        if (lastClickedPos.y == 0) y = transform.position.y;
        else y = transform.position.y + clickedPos.y - lastClickedPos.y;

        transform.position = new Vector3(clickedPos.x, y, clickedPos.z);

        lastClickedPos = clickedPos;
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
