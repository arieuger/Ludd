using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    [SerializeField] private GameObject pauseScreen;

    void Awake() {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (pauseScreen.activeInHierarchy)
                PauseGame(false);
            else 
                PauseGame(true);
        }
        
    }

    private void PauseGame(bool status) {
        pauseScreen.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
