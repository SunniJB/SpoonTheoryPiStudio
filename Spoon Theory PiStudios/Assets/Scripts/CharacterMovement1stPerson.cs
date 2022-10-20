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
    [SerializeField] float headBobbingSpeed;
    bool headBobbingUp, moving;
    float initialCamY;
    Camera cam;

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
        cam = Camera.main;
        initialCamY = cam.transform.position.y;
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

        if (x != 0 && z != 0) moving = true;
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
        if (cam.transform.position.y < initialCamY + characterInteractor.headBobbing)
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + headBobbingSpeed * Time.deltaTime, cam.transform.position.z);
        else headBobbingUp = false;
    }
    void HeadBobbingDown()
    {
        if (cam.transform.position.y > initialCamY - characterInteractor.headBobbing)
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - headBobbingSpeed * Time.deltaTime, cam.transform.position.z);
        else headBobbingUp = true;
    }
}
