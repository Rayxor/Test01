using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15;
    public Vector2 limits = new Vector2(5, 3.5f);
    
   
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        float horDirection = Input.GetAxisRaw("Horizontal");
        Vector3 horMove = Vector3.zero;
        if (transform.position.x > -limits.x && transform.position.x < limits.x){
            horMove = horDirection * Vector3.right;
            int limit = (int) transform.position.x;
        }

        float verDirection = Input.GetAxisRaw("Vertical");
        Vector3 verMove = verDirection * Vector3.up;

        transform.Translate((horMove + verMove).normalized * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other) { 
            Debug.Log("Colisiono");
        }
    }
}
