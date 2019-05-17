using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysicsMove : MonoBehaviour
{
    public float moveSpeed = 15;
    public Vector2 limits = new Vector2(10, 7);
    Vector2 ShapeLimits { get { return limits - ((colliderSize * transform.localScale) / 2); } }
    Vector2 colliderSize;
    Rigidbody2D rb2D;
    Vector2 currentMouseWorldPos;

    public Vector2 mousePlayerDelta { get { return currentMouseWorldPos - rb2D.position; } }

    // disponible como un campo en el componente script
    public GameObject bulletPrefab;

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
            movement.x = Mathf.Clamp(movement.x, -ShapeLimits.x, ShapeLimits.x);
            movement.y = Mathf.Clamp(movement.y, -ShapeLimits.y, ShapeLimits.y);
            rb2D.MovePosition (movement);
        }
        

    }

    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Colisiono");
    }

    void Update () {
        currentMouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Bang");
            Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.15f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, limits * 2);
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(Vector3.zero, ShapeLimits * 2);
        Gizmos.DrawLine(transform.position, currentMouseWorldPos);
        Gizmos.DrawSphere(currentMouseWorldPos, 0.25f);
    }

}
