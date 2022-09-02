using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Camera cam;

    [SerializeField] float desiredRotationSpeed = 0.1f, allowPlayerRotation = 0.1f;
    [SerializeField] float velocity = 5, verticalVel, gravityForce = 9.8f;

    bool isGrounded;

    Vector3 desiredMoveDirection;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        //Calculate the Input Magnitude
        float speed = new Vector2(x, z).sqrMagnitude;

        if (speed > allowPlayerRotation)
        {
            //Physically move player
            desiredMoveDirection = forward * z + right * x;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), desiredRotationSpeed);
            controller.Move(desiredMoveDirection * Time.deltaTime * velocity);
        }

        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            verticalVel = 0;
        }
        else
        {
            verticalVel = -1;
        }

        Vector3 moveVector = new Vector3(0, verticalVel * gravityForce * Time.deltaTime, 0);
        controller.Move(moveVector);
    }
}
