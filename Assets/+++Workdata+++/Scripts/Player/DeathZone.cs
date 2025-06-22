using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameObject lostPanel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Wenn Spieler in die Zone fällt
        {
            Time.timeScale = 0f; // Stoppe das Spiel
            if (lostPanel != null)
            {
                lostPanel.SetActive(true); // Zeige das Lost-Panel
            }
        }
    }
}