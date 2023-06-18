using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conscience : MonoBehaviour {

    [SerializeField] public float totalConscience;
    [SerializeField] private float recoverTimeByPoint;

    public float currentConscience { get; private set; }

    private bool isRecovering = false;

    private void Start() {
        currentConscience = 0f;
    }

    public void AddConscience(float quantity) {
        currentConscience += quantity;
        if (!isRecovering)
            StartCoroutine(RecoverInconscience());
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
}
