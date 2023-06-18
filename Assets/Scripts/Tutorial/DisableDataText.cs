using UnityEngine;

public class DisableDataText : MonoBehaviour {

    private bool canHide = false;

    void Update() {
        if (Input.GetButtonDown("Interaction") && canHide)
            GetComponent<LerpAlpha>().isDisabling = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            canHide = true;
        }
    }
}
