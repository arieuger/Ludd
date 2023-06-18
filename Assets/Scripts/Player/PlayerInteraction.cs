using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour {


    [SerializeField] private int targetDataSaves;
    [SerializeField] private TMP_Text textTargetDatasabes;
    private int currentDataSaves = 0;
    private Data dataPlace;


    private void Start() {
        textTargetDatasabes.text = ($"{currentDataSaves}/{targetDataSaves}");
    }

    private void Update() {
        if (Input.GetButtonDown("Interaction")) {
            if (dataPlace != null) {
                dataPlace.DeactivateDataPoint();
                textTargetDatasabes.text = ($"{++currentDataSaves}/{targetDataSaves}");
                GetComponent<PlayerRespawn>().ActivateCheckpoint(dataPlace.gameObject.transform);
            }
                
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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DisappearingPlatform") && other.gameObject.activeSelf) {
            StartCoroutine(other.gameObject.GetComponentInParent<DissapearingPlatform>().Dissapear());
        }
    }
}
