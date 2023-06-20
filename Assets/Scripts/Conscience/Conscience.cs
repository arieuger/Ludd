using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conscience : MonoBehaviour {

    [SerializeField] public float totalConscience;
    [SerializeField] private float recoverTimeByPoint;

    public float currentConscience { get; private set; }
    public bool conscienceAllowsMovement { get; private set; }

    private bool isRecovering = false;

    private Material mat;
    private Color starterLightColor;

    private void Start() {
        mat = GetComponent<SpriteRenderer>().material;
        starterLightColor = mat.GetColor("_GlowColor");
        currentConscience = 0f;
        conscienceAllowsMovement = true;
        StartCoroutine(CheckConscience());
    }

    public void AddConscience(float quantity) {
        if (currentConscience + quantity <= totalConscience) {
            currentConscience += quantity;
            if (!isRecovering)
                StartCoroutine(RecoverInconscience());
        }
    }

    public void RestoreInconscience() {
        currentConscience = 0f;
        StopCoroutine(RecoverInconscience());
    }

    private IEnumerator RecoverInconscience() {
        isRecovering = true;
        
        yield return new WaitForSeconds(recoverTimeByPoint);
        
        while (currentConscience > 0f) {
            currentConscience -= 1f;
            yield return new WaitForSeconds(recoverTimeByPoint);
        }
        if (currentConscience > 0f)
            currentConscience = 0f;
        
        isRecovering = false;
    }

    private IEnumerator CheckConscience() {
        while (true) {
            conscienceAllowsMovement = Random.value + 0.1f > currentConscience / 10;
            if (!conscienceAllowsMovement)
                mat.SetColor("_GlowColor", Color.red * 4);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            
            conscienceAllowsMovement = true;
            mat.SetColor("_GlowColor", starterLightColor);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
    }
}
