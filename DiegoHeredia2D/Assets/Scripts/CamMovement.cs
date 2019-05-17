using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public Transform followTarget;
    public float followSpeed;
    float minSpeed = 20;
    Vector2 camUnitDimentions;
    public PlayerPhysicsMove playerMovement;
    public float maxTargetDistance = 3.5f;
    Vector2 followPos { get { return followTarget.position; } }
    Vector2 cam2DPos { get { return (transform.position); } }
    Vector2 Limits { get { return playerMovement.limits - camUnitDimentions; } }
    Vector2 camFollowPoint { get {
            return followPos + playerMovement.mousePlayerDelta.normalized *
                Mathf.Clamp (playerMovement.mousePlayerDelta.magnitude, 0, maxTargetDistance);
                 } }

    // Start is called before the first frame update
    void Start () {
        
        camUnitDimentions = new Vector2 (Camera.main.orthographicSize * 16/9, Camera.main.orthographicSize);
    }

    // Update is called once per frame
    void LateUpdate () {
        if (followTarget) {

            Vector3 temp = Vector3.MoveTowards(transform.position, camFollowPoint, minSpeed + (camFollowPoint - cam2DPos).magnitude * followSpeed * Time.deltaTime);
            temp.x = Mathf.Clamp(temp.x, -Limits.x, Limits.x);
            temp.y = Mathf.Clamp(temp.y, -Limits.y, Limits.y);
            temp.z = transform.position.z;
            transform.position = temp;
        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(cam2DPos, camFollowPoint);
        Gizmos.DrawWireSphere
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere (followPos + camFollowPoint, 0.35f);
    }
}
