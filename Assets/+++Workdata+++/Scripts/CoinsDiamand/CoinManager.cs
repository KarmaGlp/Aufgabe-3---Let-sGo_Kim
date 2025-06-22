using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int counterCoins = 0;                     // Anzahl gesammelter Coins
    [SerializeField] private TextMeshProUGUI coinsText;                // UI-Text für die Anzeige
    [SerializeField] private GameManager gameManager;                  // Referenz zum GameManager
    public int MaxCoins => gameManager != null ? gameManager.coins.Length : 0; // maximale Coins

    private void Start()
    {
        counterCoins = 0;
        UpdateCoinText();
    }

    public void AddCoin()
    {
        counterCoins++;
        UpdateCoinText();

        // Prüfen, ob alle Coins gesammelt wurden
        if (counterCoins == MaxCoins)
        {
            gameManager.WinGame();
        }
    }

    public void ResetCoins()
    {
        counterCoins = 0;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinsText != null)
        {
            coinsText.text = counterCoins + "/" + MaxCoins;
        }
    }
}