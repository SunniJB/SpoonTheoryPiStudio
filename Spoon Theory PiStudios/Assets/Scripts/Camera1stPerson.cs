using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1stPerson : MonoBehaviour
{
    [SerializeField] CharacterMovement1stPerson player;
    [SerializeField] float mouseSensitivity = 100;

    float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        xRotation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.canMove)
        {
            MoveCamera();
        }
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
