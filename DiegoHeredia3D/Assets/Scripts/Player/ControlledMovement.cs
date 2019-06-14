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
    bool grounded { get { return groundCount > 0; } }
    int groundCount { get { return groundCollection.Count; } }
    List<Collider> groundCollection = new List<Collider>();
    public class Ground { }
    bool persistence;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool grounded = Physics.Raycast(transform.GetChild(0).position, Vector3.down, 0.15f);

        if (grounded)
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
                Debug.Log(verticalSpeed);
            }
        }
        Vector3 forwardAxis = transform.forward * speed * Input.GetAxis("Vertical");
        Vector3 verticalAxis = Vector3.up * verticalSpeed;
        Vector3 horizontal = Vector3.up * Input.GetAxis("Horizontal");

        playerAnimator.SetBool("Grounded", grounded);

        Debug.Log(Vector3.Dot(Vector3.up, Vector3.up));

        characterController.Move((forwardAxis + verticalAxis) * Time.deltaTime);
        transform.Rotate(horizontal * angularSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collider" + collision.collider.name);
        Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal, Color.red);

        for (int i = 0; i < collision.contactCount; i++)
        {
            if (Vector3.Dot(collision.contacts[i].normal, Vector3.up) > 0.80)
            {
                if (!groundCollection.Contains(collision.collider))
                {
                    groundCollection.Add(collision.collider);
                }
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        //Ground exitGround = groundCollection.Find(ground => ground.collider == collision.collider);
        //if (exitGround != null)
        {
            //persistence = Physics.Raycast();
        }

        Debug.Log(persistence);
        StartCoroutine(ReCheckPersistance());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.GetChild(0).position, Vector3.down * 0.15f);
    }
    
    IEnumerator ReCheckPersistance()
    {
        yield return new WaitForSeconds(0.2f);
        persistence = groundCount > 0;
    }
}
