using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class PlayerInteraction : MonoBehaviour {

    private float maxIntensity;
    private float minIntensity = 1.7f;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("DataPlace")) {
            other.GetComponent<Animator>().SetTrigger("Down");
            Light2D lightComponent = other.GetComponentInChildren<Light2D>();
            maxIntensity = lightComponent.intensity;
            StartCoroutine(Twinkle(lightComponent));
        }
    }

    private IEnumerator Twinkle(Light2D lightComponent) {
        for (int i = 0; i < Random.Range(4,10); i++) {
            lightComponent.intensity = Random.Range(minIntensity, minIntensity + 0.3f);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));

            lightComponent.intensity = maxIntensity;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.35f));
        }
        lightComponent.enabled = false;
    }

}
