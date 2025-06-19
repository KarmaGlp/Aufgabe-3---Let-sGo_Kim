using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject lostPanel; // UI-Panel, das bei Spielverlust angezeigt wird
    public GameObject winPanel;  // UI-Panel, das bei Spielsieg angezeigt wird

    public GameObject startCountdownPanel;     // Panel, das den Countdown enthält
    public TextMeshProUGUI countdownText;      // Textfeld für den Countdown-Zähler

    public PlayerMovement2D playerMovement;    // Referenz auf das Skript zur Spielerbewegung
    public GameObject[] coins;                 // Alle Coins, die in der Szene vorhanden sind

    private void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin"); // Finde alle Coins anhand ihres Tags
        lostPanel.SetActive(false); // Verstecke das "Verloren"-Panel zu Beginn
        winPanel.SetActive(false);  // Verstecke das "Gewonnen"-Panel zu Beginn
        startCountdownPanel.SetActive(false); // Countdown-Panel zu Beginn nicht anzeigen

        StartCoroutine(CountdownCoroutine()); // Starte den Countdown beim Spielstart
    }

    private IEnumerator CountdownCoroutine()
    {
        if (playerMovement != null)
            playerMovement.enabled = false; // Deaktiviere die Spielerbewegung während des Countdowns

        startCountdownPanel.SetActive(true); // Zeige das Countdown-Panel an

        int count = 3; // Startwert für den Countdown
        while (count > 0)
        {
            countdownText.text = count.ToString(); // Setze die aktuelle Zahl im Text

            // === Animationseffekt starten ===
            countdownText.color = GetCountdownColor(count); // Ändere Farbe je nach Zahl
            countdownText.rectTransform.localScale = Vector3.one * 2f; // Skalierung auf 200%
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 4f; // Schneller zurückskalieren
                countdownText.rectTransform.localScale = Vector3.Lerp(Vector3.one * 2f, Vector3.one, t); // Skaliere zurück auf normal
                yield return null;
            }
            // === Animationseffekt endet ===

            yield return new WaitForSeconds(0.5f); // Warte bevor nächste Zahl kommt
            count--; // Reduziere den Zähler
        }

        countdownText.text = "Go!"; // Zeige "Go!" am Ende des Countdowns
        countdownText.color = Color.green; // "Go!" wird grün dargestellt
        countdownText.rectTransform.localScale = Vector3.one * 2f; // Skaliere "Go!" etwas größer

        yield return new WaitForSeconds(1f); // Zeige "Go!" für eine Sekunde

        startCountdownPanel.SetActive(false); // Blende das Countdown-Panel aus

        if (playerMovement != null)
            playerMovement.enabled = true; // Aktiviere die Spielerbewegung

        StartGame(); // Initialisiere das Spiel
    }

    // Gibt je nach Countdown-Zahl eine passende Farbe zurück
    private Color GetCountdownColor(int count)
    {
        switch (count)
        {
            case 3: return Color.yellow; // 3 = Gelb
            case 2: return new Color(1f, 0.5f, 0f); // 2 = Orange
            case 1: return Color.red; // 1 = Rot
            default: return Color.white;
        }
    }

    public void StartGame()
    {
        GetComponent<CoinManager>().ResetCoins(); // Setze die Anzahl gesammelter Coins zurück
        lostPanel.SetActive(false); // Stelle sicher, dass das Verloren-Panel ausgeblendet ist
    }

    public void WinGame()
    {
        lostPanel.SetActive(false); // Verstecke das Verloren-Panel (zur Sicherheit)
        winPanel.SetActive(true);   // Zeige das Gewinn-Panel an
    }

    public void ShowLostPanel()
    {
        lostPanel.SetActive(true); // Aktiviere das "Verloren"-Panel

        if (playerMovement != null)
            playerMovement.enabled = false; // Stoppe die Spielerbewegung

        foreach (var coin in coins)
        {
            coin.SetActive(true); // Aktiviere alle Coins erneut (z. B. für Neustart)
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Lade die aktuelle Szene neu
    }
}

