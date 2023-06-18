using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {

    [SerializeField] private SpriteRenderer[] renders;
    private BoxCollider2D platformCollider;


    // Start is called before the first frame update
    void Start()
    {
        renders = GetComponentsInChildren<SpriteRenderer>();
        platformCollider = GetComponentInChildren<BoxCollider2D>();
    }

    public IEnumerator Dissapear() {
        for (int i = 0; i < Random.Range(3,6); i++) {
            foreach (SpriteRenderer render in renders) {
                render.color = Color.grey;
                render.enabled = false;
                yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));

                render.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.03f, 0.07f));
            }
            // light2D.intensity = Random.Range(minIntensity, minIntensity + 0.3f);
            // yield return new WaitForSeconds(Random.Range(0.01f, 0.2f));

            // light2D.intensity = maxIntensity;
            // yield return new WaitForSeconds(Random.Range(0.05f, 0.35f));
        }
        
        foreach (SpriteRenderer render in renders) {
            render.enabled = false;
        }
        platformCollider.enabled = false;
    }
}
