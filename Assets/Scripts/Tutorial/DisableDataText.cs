using UnityEngine;

public class DisableDataText : MonoBehaviour {

    void Update() {
        if (Input.GetButtonDown("Interaction"))
            GetComponent<LerpAlpha>().isDisabling = true;
    }
}
