using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("Enemy")) {
            other.GetComponent<Health>().TakeDamage(50f);
        }
    }

}
