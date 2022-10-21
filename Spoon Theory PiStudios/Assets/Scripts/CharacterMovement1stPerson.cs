using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement1stPerson : MonoBehaviour
{
    CharacterController controller;

    [Header("PLAYER ATTRIBUTES")]
    [SerializeField] float speed = 5, gravityForce = 9.8f;

    [Header("HEAD BOBBING")]
    [SerializeField] float headBobbingSpeed, headBobbingAmount;
    [SerializeField] Transform camParent;
    public bool headBobbingUp, moving;
    float initialCamY;

    CharacterInteractor characterInteractor;

    float verticalVelocity;

    bool isGrounded;

    public bool canMove;


    private void Awake()
    {
        characterInteractor = GetComponent<CharacterInteractor>();
    }

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        headBobbingUp = true;
        moving = false;

        controller = GetComponent<CharacterController>();
        initialCamY = camParent.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) Movement();

        if (moving) HeadBobbing();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (x != 0 || z != 0) moving = true;
        else moving = false;

        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
             
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * characterInteractor.speedMultiplier * Time.deltaTime );
        verticalVelocity -= gravityForce * Time.deltaTime;
        controller.Move(transform.up * verticalVelocity * Time.deltaTime );
    }

    void HeadBobbing()
    {
        if (headBobbingUp) HeadBobbingUp();
        else HeadBobbingDown();
    }

    void HeadBobbingUp()
    {
        if (camParent.position.y < initialCamY + headBobbingAmount * characterInteractor.headBobbingMultiplier)
            camParent.transform.position = new Vector3(camParent.position.x, camParent.position.y + headBobbingSpeed * characterInteractor.headBobbingMultiplier * Time.deltaTime, camParent.position.z);
        else headBobbingUp = false;
    }
    void HeadBobbingDown()
    {
        if (camParent.position.y > initialCamY - headBobbingAmount * characterInteractor.headBobbingMultiplier)
            camParent.transform.position = new Vector3(camParent.position.x, camParent.position.y - headBobbingSpeed * characterInteractor.headBobbingMultiplier * Time.deltaTime, camParent.position.z);
        else headBobbingUp = true;
    }
}
