using UnityEngine;

public class DisableAttackText : MonoBehaviour {

    private bool canHide = false;
    void Update() {
        if (Input.GetButtonDown("Fire1") && canHide)
            GetComponent<LerpAlpha>().isDisabling = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canHide = true;
        }
    }
}
