using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearingPlatform : MonoBehaviour {

    [SerializeField] private SpriteRenderer[] renders;
    private BoxCollider2D platformCollider;


    // Start is called before the first frame update
    void Start()
    {
        // renders = GetComponentsInChildren<SpriteRenderer>();
        platformCollider = GetComponentInChildren<BoxCollider2D>();
    }

    public IEnumerator Dissapear() {
        for (int i = 0; i < Random.Range(3,6); i++) {
            foreach (SpriteRenderer render in renders) {
                if (render != null && !render.gameObject.CompareTag("Player")) {
                    render.color = Color.grey;
                    render.enabled = false;
                    yield return new WaitForSeconds(Random.Range(0.01f, 0.05f));

                    render.enabled = true;
                    yield return new WaitForSeconds(Random.Range(0.03f, 0.07f));
                    render.color = Color.white;
                }
            }
        }
        
        foreach (SpriteRenderer render in renders) {
           // render.enabled = false;
            if (render != null && render.GetComponentInParent<DissapearingPlatform>() != null) {
                if (render.GetComponentInChildren<PlayerMovement>() != null) {
                    render.GetComponentInChildren<PlayerMovement>().gameObject.transform.parent = null;
                }
                render.GetComponentInParent<DissapearingPlatform>().gameObject.SetActive(false);
            }
        }
        // platformCollider.enabled = false;
    }
}
