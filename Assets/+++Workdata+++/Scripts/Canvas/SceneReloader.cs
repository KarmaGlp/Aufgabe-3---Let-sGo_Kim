using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        Time.timeScale = 1f; // Falls das Spiel pausiert wurde (z.B. bei Lost Panel)
        Scene currentScene = SceneManager.GetActiveScene(); // Aktuelle Szene holen
        SceneManager.LoadScene(currentScene.buildIndex); // Szene über BuildIndex neu laden
    }
}