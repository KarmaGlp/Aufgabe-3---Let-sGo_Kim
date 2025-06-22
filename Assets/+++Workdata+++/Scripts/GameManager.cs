using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject lostPanel;
    public GameObject winPanel;  

    public GameObject startCountdownPanel; 
    public TextMeshProUGUI countdownText;  

    public PlayerMovement2D playerMovement; 
    public GameObject[] coins;              
    public GameObject[] diamands;           

    public float gameDuration = 10f; // Spieldauer in Sekunden
    private float timer;             
    private bool isTimerRunning = false; 

    public Slider timeSlider;      
    public Image fillImage;        

    private void Start()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");
        diamands = GameObject.FindGameObjectsWithTag("Diamand");

        lostPanel.SetActive(false);
        winPanel.SetActive(false);
        startCountdownPanel.SetActive(false);

        SetEnemyMovement(false);     // Gegner sollen zu Beginn NICHT laufen
        SetPlatformMovement(false);  // Plattformen sollen ebenfalls stillstehen

        StartCoroutine(CountdownCoroutine()); // Starte Countdown
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timeSlider != null)
            {
                timeSlider.value = timer;
                float t = timer / gameDuration;
                Color barColor = Color.Lerp(Color.red, Color.green, t);
                fillImage.color = barColor;
            }

            if (timer <= 0f)
            {
                isTimerRunning = false;
                ShowLostPanel();
            }
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        if (playerMovement != null)
            playerMovement.enabled = false;

        SetEnemyMovement(false);
        SetPlatformMovement(false);

        startCountdownPanel.SetActive(true);
        int count = 3;

        while (count > 0)
        {
            countdownText.text = count.ToString();
            countdownText.color = GetCountdownColor(count);
            countdownText.rectTransform.localScale = Vector3.one * 2f;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * 4f;
                countdownText.rectTransform.localScale = Vector3.Lerp(Vector3.one * 2f, Vector3.one, t);
                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
            count--;
        }

        countdownText.text = "Go!";
        countdownText.color = Color.green;
        countdownText.rectTransform.localScale = Vector3.one * 2f;

        yield return new WaitForSeconds(1f);
        startCountdownPanel.SetActive(false);

        if (playerMovement != null)
            playerMovement.enabled = true;

        StartGame();
    }

    private Color GetCountdownColor(int count)
    {
        switch (count)
        {
            case 3: return Color.yellow;
            case 2: return new Color(1f, 0.5f, 0f);
            case 1: return Color.red;
            default: return Color.white;
        }
    }

    public void StartGame()
    {
        GetComponent<CoinManager>().ResetCoins();
        GetComponent<DiamandManager>().ResetDiamands();

        lostPanel.SetActive(false);
        timer = gameDuration;
        isTimerRunning = true;

        if (timeSlider != null)
        {
            timeSlider.maxValue = gameDuration;
            timeSlider.value = gameDuration;
        }

        SetEnemyMovement(true);     // Gegner bewegen sich
        SetPlatformMovement(true);  // Plattformen bewegen sich
    }

    public void WinGame()
    {
        lostPanel.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ShowLostPanel()
    {
        lostPanel.SetActive(true);
        isTimerRunning = false;

        if (playerMovement != null)
            playerMovement.enabled = false;

        SetEnemyMovement(false);
        SetPlatformMovement(false); // Plattformen stoppen bei Game Over

        foreach (var coin in coins)
        {
            coin.SetActive(true);
        }

        foreach (var diamand in diamands)
        {
            diamand.SetActive(true);
        }

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Gegnerbewegung aktivieren/deaktivieren
    private void SetEnemyMovement(bool active)
    {
        foreach (EnemyPatrol enemy in FindObjectsOfType<EnemyPatrol>())
        {
            enemy.canMove = active;
        }
    }

    // Plattformbewegung aktivieren/deaktivieren
    private void SetPlatformMovement(bool active)
    {
        foreach (MovingPlatform platform in FindObjectsOfType<MovingPlatform>())
        {
            if (active)
                platform.StartMoving();
            else
                platform.StopMoving();
        }
    }
}
