using UnityEngine;

public class PlayerInteraction : MonoBehaviour {

    private Data dataPlace;

    private void Update() {
        if (Input.GetButtonDown("Interaction")) {
            if (dataPlace != null)
                dataPlace.DeactivateDataPoint();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DataPlace")) {
            dataPlace = other.transform.GetComponent<Data>();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("DataPlace")) {
            dataPlace = null;
        }
    }
}
