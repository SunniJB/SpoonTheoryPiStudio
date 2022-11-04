using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragRotation : MonoBehaviour, IDragHandler
{
    [SerializeField] Camera camRendering;
    public Transform objectToRotate;
    [SerializeField] float minFov = 30f, maxFov = 90f, sensitivity = 10f;

    void  Update()
    {
        float fov = camRendering.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        camRendering.fieldOfView = fov;
    }

    public void OnDrag(PointerEventData eventData)
    {
        objectToRotate.eulerAngles += new Vector3(-eventData.delta.y, -eventData.delta.x);
    }

}
