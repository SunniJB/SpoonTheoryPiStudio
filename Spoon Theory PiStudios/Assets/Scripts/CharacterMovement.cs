using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController controller;
    Camera cam;

    [SerializeField] CinemachineFreeLook freeLook;
    private string xAxisName = "Mouse X";
    private string yAxisName = "Mouse Y";

    [SerializeField] float desiredRotationSpeed = 0.1f, allowPlayerRotation = 0.1f;
    [SerializeField] float velocity = 5, verticalVel, gravityForce = 9.8f;

    bool isGrounded, canMove;

    Vector3 desiredMoveDirection;

    private void Awake()
    {
        canMove = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
            //enables camera following cursor
            freeLook.m_XAxis.m_InputAxisName = xAxisName;
            freeLook.m_YAxis.m_InputAxisName = yAxisName;
        }
        else
        {
            //disables camera following cursor
            freeLook.m_XAxis.m_InputAxisName = "";
            freeLook.m_XAxis.m_InputAxisValue = 0;

            freeLook.m_YAxis.m_InputAxisName = "";
            freeLook.m_YAxis.m_InputAxisValue = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            canMove = !canMove;
        }
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
