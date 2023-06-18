using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalWrapper : MonoBehaviour
{
    private GameObject player;
    private Animator animator;
    [SerializeField] private GameObject endCanvas;
    
    private void Start() {
        animator = GetComponent<Animator>();
    }
    
    void Update() {
        if (player != null && player.GetComponent<PlayerInteraction>().currentDataSaves >= 3) {
            transform.position = new Vector2(player.transform.position.x, transform.position.y);
            StartCoroutine(StartWrap());
        }
    }

    public void DeactivatePlayer() {
        player.SetActive(false);
        StartCoroutine(EndGame());
    }

    private IEnumerator StartWrap() {
        yield return new WaitForSeconds(1.5f);
        animator.SetTrigger("wrap");
    }

    private IEnumerator EndGame() {
        yield return new WaitForSeconds(2.5f);
        endCanvas.SetActive(true);
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(0);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            player = null;
        }
    }
}
