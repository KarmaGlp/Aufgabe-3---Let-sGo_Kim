using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public PlayerMovement2D player;

    public void OpenPauseMenu()
    {
        pausePanel.SetActive(true);
    Time.timeScale = 0;

    if (player !=null)
    {
        player = FindObjectOfType<PlayerMovement2D>();
    }

    if (player != null)
        player.SetCanMove(false);
    }

    public void ClosePauseMenu()
    {
       pausePanel.SetActive(false);
    Time.timeScale = 1;

    if (player == null)
    {
        player = FindObjectOfType<PlayerMovement2D>();
    }

    if (player != null)
        player.SetCanMove(true);
    }
}
