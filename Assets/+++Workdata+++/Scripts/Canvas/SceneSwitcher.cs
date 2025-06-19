using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Debug.Log("Button gedrückt – lade Szene: " + index);
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }
}
