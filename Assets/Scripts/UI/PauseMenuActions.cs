using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuActions : MonoBehaviour
{
    public void OnRestart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackToMenu() {
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton() {
        Debug.Log("Quit");
        Application.Quit();
    }
}
