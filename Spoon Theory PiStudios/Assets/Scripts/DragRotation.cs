using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class DragRotation : MonoBehaviour, IDragHandler
{
    [SerializeField] Camera camRendering;
    public Transform objectToRotate;
    [SerializeField] float minFov = 30f, maxFov = 90f, sensitivity = 10f;
    public TMP_Text prompt;

    [HideInInspector] public float yaw, pitch;

    void  Update()
    {
        float fov = camRendering.fieldOfView;
        fov -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        camRendering.fieldOfView = fov;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //yaw = Mathf.Clamp(yaw - eventData.delta.y * sensitivity * Time.deltaTime, -180f, 180f ); // change 120 to whatever you want to clamp
        //pitch = Mathf.Clamp(pitch - eventData.delta.x * sensitivity * Time.deltaTime, -180f, 180f);
        yaw -= eventData.delta.y * sensitivity * Time.deltaTime;
        pitch += eventData.delta.x * sensitivity * Time.deltaTime;

        objectToRotate.rotation = Quaternion.Euler(yaw, pitch, 0f);
        //objectToRotate.Rotate(eventData.delta.y * sensitivity * Time.deltaTime, eventData.delta.x * sensitivity * Time.deltaTime, 0, Space.Self);
        //objectToRotate.eulerAngles += new Vector3(eventData.delta.y, -eventData.delta.x);
    }

}
