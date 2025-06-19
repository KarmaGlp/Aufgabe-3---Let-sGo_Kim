using UnityEngine;
using TMPro;

public class DiamandManager : MonoBehaviour
{
    private int collectedDiamands = 0;         // Anzahl eingesammelter Diamanten
    private int totalDiamandsInScene = 0;      // Anzahl aller Diamanten in der Szene

    public TextMeshProUGUI diamandText;        // UI-Textfeld zur Anzeige der Zahl

    // Öffentliche Eigenschaften für Zugriff von außen (z. B. GameManager)
    public int CollectedDiamands => collectedDiamands;
    public int TotalDiamandsInScene => totalDiamandsInScene;

    void Start()
    {
        // Finde alle GameObjects mit dem Tag "Diamand" in der aktuellen Szene
        totalDiamandsInScene = GameObject.FindGameObjectsWithTag("Diamand").Length;
        UpdateDiamandText();
    }

    public void AddDiamand()
    {
        collectedDiamands++;
        UpdateDiamandText();
    }

    private void UpdateDiamandText()
    {
        if (diamandText != null)
        {
            diamandText.text = $"{collectedDiamands}/{totalDiamandsInScene}";
        }
    }
}