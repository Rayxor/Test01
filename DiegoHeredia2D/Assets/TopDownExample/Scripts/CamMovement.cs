using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float followSpeed;
    public float minSpeed = 3.2f;
    Vector2 camUnitDimentions;
    public PlayerPhysicsMove playerMovement;
    public float maxTargetDistance = 3.5f;
    Vector2 cam2DPos { get { return transform.position; } }
    Vector2 limits { get { return playerMovement.limits - camUnitDimentions; } }
    Vector2 camFollowPoint { get {
            return playerMovement.current2DPos + playerMovement.mousePlayerDelta.normalized *
                Mathf.Clamp(playerMovement.mousePlayerDelta.magnitude, 0, maxTargetDistance);
        }
    }

    Vector2 tempFollowPoint { get { return tempTarget.position; } }
    Transform tempTarget;
    float tempSpeed;

    float targetOrtographicSize = 5;

    // Start is called before the first frame update
    void Start () {
         camUnitDimentions = new Vector2 (Camera.main.orthographicSize * 16/9, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void LateUpdate () {
        if (playerMovement) {

            Vector3 temp = Vector3.MoveTowards(transform.position, camFollowPoint, minSpeed + (camFollowPoint - cam2DPos).magnitude * followSpeed * Time.deltaTime);
            temp.x = Mathf.Clamp(temp.x, -limits.x, limits.x);
            temp.y = Mathf.Clamp(temp.y, -limits.y, limits.y);
            temp.z = transform.position.z;
            transform.position = temp;
        }
    }

    public void SetTempTarget(Transform target = null, float speed = 0, float size =5)
    {
        tempTarget = target;
        tempSpeed = speed;
        targetOrtographicSize = size;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cam2DPos, camFollowPoint);
        Gizmos.DrawWireSphere(cam2DPos, 0.35f);
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(camFollowPoint, 0.35f);
    }
}

