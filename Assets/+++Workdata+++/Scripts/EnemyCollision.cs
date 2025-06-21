using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Prüft, ob der Spieler den Gegner berührt
        {
            Debug.Log("Spieler hat den Gegner berührt");

            GameManager gameManager = FindObjectOfType<GameManager>(); // Sucht GameManager
            if (gameManager != null)
            {
                gameManager.ShowLostPanel(); // Zeigt das Lost-Panel

                EnemyPatrol patrol = GetComponent<EnemyPatrol>(); // Holt das Bewegungs-Script
                if (patrol != null)
                {
                    patrol.StopMoving(); // Stoppt Bewegung
                }
            }
            else
            {
                Debug.LogWarning("Kein GameManager gefunden!");
            }
        }
    }
}