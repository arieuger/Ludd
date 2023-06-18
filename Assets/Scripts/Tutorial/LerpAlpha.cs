using System.Collections;
using UnityEngine;
using TMPro;

public class LerpAlpha : MonoBehaviour {

    private TMP_Text text;

    [HideInInspector] public bool isDisabling = false;

    // Start is called before the first frame update
    void Start() {
        text = GetComponent<TMP_Text>();
        StartCoroutine(LerpAlphaCo());
    }

    public IEnumerator LerpAlphaCo() {

        float duration = 1.5f;
        float lerpTimer = 0f;
        float minAlpha = 0.1f;
        float maxAlpha = 0.7f;

        while (!isDisabling) {
            Color color = text.color;
            lerpTimer += Time.deltaTime;
            float lerp = Mathf.PingPong(lerpTimer, duration) / duration;
            color.a = Mathf.Lerp(minAlpha, maxAlpha, Mathf.SmoothStep(minAlpha, maxAlpha, lerp));
            text.color = color;
            yield return null;
        }

        StartCoroutine(DisableText());
    }

    public IEnumerator DisableText() {
        while (text.color.a >= 0.05f) {
            Color color = text.color;
            color.a -= Time.deltaTime;
            text.color = color;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
