using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement1stPerson : MonoBehaviour
{
    CharacterController controller;

    [Header("PLAYER ATTRIBUTES")]
    [SerializeField] float speed = 5, gravityForce = 9.8f;

    float verticalVelocity;

    bool isGrounded, canMove;


    private void Awake()
    {
        canMove = true;

        controller = GetComponent<CharacterController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
             

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed *Time.deltaTime );
        verticalVelocity -= gravityForce * Time.deltaTime;
        controller.Move(transform.up * verticalVelocity * Time.deltaTime );
    }
}
