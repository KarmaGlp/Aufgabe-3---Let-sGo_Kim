using UnityEngine; 

public class PlayerMovement2D : MonoBehaviour // für 2D-Spielerbewegung
{
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit
    public float jumpForce = 10f; // Sprungkraft

    public Transform groundCheck; // Position zu Boden
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f); // Größe des groundCheck

    public LayerMask groundLayer; // Layer-Maske für Boden
    public CoinManager coinManager; // Referenz auf CoinManager

    public DiamandManager diamandManager; // Referenz für DiamandManager
    private Rigidbody2D rb; // Rigidbody-Komponente

    private bool isGrounded; // Ist der Spieler am Boden?
    private bool canMove = true; // ist Bewegung erlaubt?

    [Header("Audio")]
    public AudioClip jumpSound; // Sound für Sprung
    [Range(0f, 1f)] public float jumpVolume = 1f; // Lautstärke für Sprung

    public AudioClip coinSound; // Sound für Münze
    [Range(0f, 1f)] public float coinVolume = 0.3f; // Lautstärke für Münze

    public AudioClip diamandSound; // Sound für Diamant
    [Range(0f, 1f)] public float diamandVolume = 1f; // Lautstärke für Diamant

    private AudioSource audioSource; // Zentrale Audioquelle

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>(); // Hole Rigidbody-Komponente
        audioSource = GetComponent<AudioSource>(); // Hole AudioSource

        if (coinManager == null) // CoinManager nicht zugewiesen?
            coinManager = FindObjectOfType<CoinManager>(); // Automatisch finden

        if (diamandManager == null) // DiamandManager nicht zugewiesen?
            diamandManager = FindObjectOfType<DiamandManager>(); // Automatisch finden
    }

    void Update() 
    {
        if (!canMove) return; // Bewegung erlaubt?

        float moveInput = Input.GetAxisRaw("Horizontal"); // Links/rechts Input
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Bewegung setzen

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer); // Prüfen, ob Boden

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Sprung gedrückt & am Boden?
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Sprung ausführen
            PlaySound(jumpSound, jumpVolume); // Sprung-Sound abspielen
        }
    }

    public void SetCanMove(bool value) // Bewegung aktivieren/deaktivieren
    {
        canMove = value;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null) // Nur wenn gesetzt
        {
            Gizmos.color = Color.green; // Farbe grün
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize); // Box zeichnen
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Kollision mit Trigger
    {
        if (other.CompareTag("Coin")) // Coin berührt?
        {
            if (coinManager != null) coinManager.AddCoin(); // Zähler erhöhen
            PlaySound(coinSound, coinVolume); // Coin-Sound abspielen
            Destroy(other.gameObject); // Coin zerstören
        }
        else if (other.CompareTag("Diamand")) // Diamant berührt?
        {
            if (diamandManager != null) diamandManager.AddDiamand(); // Zähler erhöhen
            PlaySound(diamandSound, diamandVolume); // Diamant-Sound abspielen
            Destroy(other.gameObject); // Diamant zerstören
        }
    }

    private void PlaySound(AudioClip clip, float volume) // Hilfsmethode für Sound
    {
        if (clip != null && audioSource != null) // Sound & AudioSource vorhanden?
            audioSource.PlayOneShot(clip, volume); // Sound mit Lautstärke abspielen
    }
}
