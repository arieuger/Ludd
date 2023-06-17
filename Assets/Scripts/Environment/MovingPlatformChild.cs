using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformChild : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("a");
        if (other.gameObject.CompareTag("Player")) {
            Debug.Log("b");
            other.gameObject.transform.SetParent(transform);
        }
            
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player"))
            other.gameObject.transform.SetParent(null);
    }

    public void UpdateDoorClosed() {}

    public void UpdateDoorOpen() {}
}
