using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledMovement : MovScript
{
    public float gravity;
    public CharacterController characterController;
    Animator playerAnimator;
    float verticalSpeed;
    public float jumpForce = 10;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit info;
        characterController.Raycast(new Ray(transform.GetChild(0).position, Vector3.down * 5), out info);
        if (!characterController.isGrounded)
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }
        else
        {
            verticalSpeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(verticalSpeed);
                verticalSpeed = jumpForce;
                playerAnimator.SetTrigger("Jump");
                Debug.Log(verticalSpeed);
            }
        }
        Vector3 forwardAxis = transform.forward * speed * Input.GetAxis("Vertical");
        Vector3 verticalAxis = Vector3.up * verticalSpeed;
        Vector3 horizontal = Vector3.up * Input.GetAxis("Horizontal");

        characterController.Move((forwardAxis + verticalAxis) * Time.deltaTime);
        transform.Rotate(horizontal * angularSpeed * Time.deltaTime);
    }
}
