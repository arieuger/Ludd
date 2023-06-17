using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Transform platform;
    [SerializeField] private float speed = 1.5f;

    private int direction = 1;

    void Update() {
        Vector2 target = CurrentMovementTarget();
        platform.position = Vector2.Lerp(platform.position, target, speed * Time.deltaTime);
        float distance = (target - (Vector2) platform.position).magnitude;

        if (distance <= 0.1f)
            direction *= -1;
    }

    private Vector2 CurrentMovementTarget() {
        return direction == 1 ? startPoint.position : endPoint.position;
    }
    
    void OnDrawGizmos() {
        if (platform != null && startPoint != null && endPoint != null) {
            Gizmos.DrawLine(platform.transform.position, startPoint.position);
            Gizmos.DrawLine(platform.transform.position, endPoint.position);
        }
    }

}
