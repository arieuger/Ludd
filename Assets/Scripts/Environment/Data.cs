using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Data : MonoBehaviour {
    
    private float maxIntensity;
    private float minIntensity = 1.7f;
    private Animator animator;
    private Light2D light2D;

    private void Start() {
        animator = GetComponent<Animator>();
        light2D = GetComponentInChildren<Light2D>();
    }

    public void DeactivateDataPoint() {
        animator.SetTrigger("Down");
        maxIntensity = light2D.intensity;
        StartCoroutine(Twinkle());
    }

        private IEnumerator Twinkle() {
        for (int i = 0; i < Random.Range(4,10); i++) {
            light2D.intensity = Random.Range(minIntensity, minIntensity + 0.3f);
            yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));

            light2D.intensity = maxIntensity;
            yield return new WaitForSeconds(Random.Range(0.05f, 0.35f));
        }
        light2D.enabled = false;
    }

}
