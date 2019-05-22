using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardLinearMovement : MonoBehaviour
{
    public float speed = 1;
    float direction = 1;
    int currentPathIndex = 0;
    public Vector3[] pathPoints;
  

    // Start is called before the first frame update
    void Start()
    {
        pathPoints = new Vector3[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.color = Color.green;
        for (int i = 0; 1< pathPoints.Length; 1++)
        {
            
        }
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.up);
        Gizmos.DrawRay(transform.position, transform.right);


    }


}
