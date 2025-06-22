using TMPro;
using UnityEngine;

public class DiamandManager : MonoBehaviour
{
    [SerializeField] private int collectedDiamands = 0;                     // Anzahl gesammelter Diamanten
    [SerializeField] private TextMeshProUGUI diamandText;                  // UI-Text für die Anzeige
    [SerializeField] private GameManager gameManager;                      // Referenz zum GameManager

    public int MaxDiamands => gameManager != null ? gameManager.diamands.Length : 0; // maximale Diamanten

    private void Start()
    {
        collectedDiamands = 0;
        UpdateDiamandText();
    }

    public void AddDiamand()
    {
        collectedDiamands++;
        UpdateDiamandText();

        // Prüfen, ob alle Diamanten gesammelt wurden
    }

    public void ResetDiamands()
    {
        collectedDiamands = 0;
        UpdateDiamandText();
    }

    private void UpdateDiamandText()
    {
        if (diamandText != null)
        {
            diamandText.text = collectedDiamands + "/" + MaxDiamands;
        }
    }
}