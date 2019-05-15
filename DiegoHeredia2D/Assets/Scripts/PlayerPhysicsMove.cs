using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsMove : MonoBehaviour
{
    public float moveSpeed = 15;
    public Vector2 limits = new Vector2(10, 7);
    Vector2 shapeLimits { get { return limits - ((colliderSize * transform.localScale) / 2); } }
    Vector2 colliderSize;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        colliderSize = gameObject.GetComponent<BoxCollider2D>().size;
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Get Current Horizontal Movement
        Vector2 horMove = Input.GetAxisRaw("Horizontal") * Vector3.right;
        //Get Current Vertical Movement
        Vector2 verMove = Input.GetAxisRaw("Vertical") * Vector3.up;

        //If chequea si existe
        if (rb2D) {
            Vector2 movement = rb2D.position + ((horMove + verMove).normalized * moveSpeed * Time.fixedDeltaTime);
            movement.x = Mathf.Clamp(movement.x, -shapeLimits.x, shapeLimits.x);
            movement.y = Mathf.Clamp(movement.y, -shapeLimits.y, shapeLimits.y);
            rb2D.MovePosition (movement);
        }
        

    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Colisiono");
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.15f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, limits * 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Vector3.zero, shapeLimits * 2);
        Gizmos.DrawSphere (Input)
    }

}
