using UnityEngine;

public class DisableAttackText : MonoBehaviour {
    void Update() {
        if (Input.GetButtonDown("Fire1"))
            GetComponent<LerpAlpha>().isDisabling = true;
    }
}
