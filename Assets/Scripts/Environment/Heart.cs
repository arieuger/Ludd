using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Heart : MonoBehaviour {

    private Light2D heartLight;
    private Animator animator;
    
    void Start() {
        heartLight = GetComponentInChildren<Light2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            animator.SetTrigger("achieve");
            heartLight.enabled = false;
            other.GetComponent<Health>().AddHealth(1f);
        }
    }

    private void LightAchieved() {
        Destroy(gameObject);
    }

}
