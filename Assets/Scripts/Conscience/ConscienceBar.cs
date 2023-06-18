using UnityEngine;
using UnityEngine.UI;

public class ConscienceBar : MonoBehaviour {

    [SerializeField] private Conscience playerConscience;
    [SerializeField] private Image totalConscienceBar;
    [SerializeField] private Image currentConscienceBar;

    void Start() {
        totalConscienceBar.fillAmount = playerConscience.totalConscience / 10;
    }

    // Update is called once per frame
    void Update() {
        currentConscienceBar.fillAmount = playerConscience.currentConscience / 10;
    }
}
