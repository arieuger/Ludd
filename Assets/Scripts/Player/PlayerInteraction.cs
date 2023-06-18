using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour {


    [SerializeField] private int targetDataSaves;
    [SerializeField] private TMP_Text textTargetDatasabes;
    public int currentDataSaves { get; private set; }
    private Data dataPlace;


    private void Start() {
        currentDataSaves = 0;
        textTargetDatasabes.text = ($"{currentDataSaves}/{targetDataSaves}");
    }

    private void Update() {
        if (Input.GetButtonDown("Interaction")) {
            if (dataPlace != null) {
                if (dataPlace.GetComponentInChildren<Light2D>().enabled) {
                    dataPlace.DeactivateDataPoint();
                    textTargetDatasabes.text = ($"{++currentDataSaves}/{targetDataSaves}");
                    GetComponent<PlayerRespawn>().ActivateCheckpoint(dataPlace.gameObject.transform);
                }
                
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
