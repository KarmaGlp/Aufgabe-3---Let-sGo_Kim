using UnityEngine; // Zugriff auf Unity-spezifische Klassen und Methoden

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit des Spielers
    public float jumpForce = 10f; // Sprungkraft nach oben

    public Transform groundCheck; // Transform-Objekt zur Bodenprüfung
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f); // Größe des Bodenkontrollbereichs

    public LayerMask groundLayer; // Welche Layer als Boden erkannt werden
    public CoinManager coinManager; // Referenz auf den CoinManager
    public DiamandManager diamandManager; // Referenz auf den DiamandManager

    private Rigidbody2D rb; // Rigidbody-Komponente des Spielers
    private bool isGrounded; // Ob der Spieler aktuell den Boden berührt

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody vom Objekt holen

        if (coinManager == null) // Wenn kein CoinManager zugewiesen wurde
        {
            coinManager = FindObjectOfType<CoinManager>(); // Versuche ihn automatisch zu finden
        }

        if (diamandManager == null) // Wenn kein DiamandManager zugewiesen wurde
        {
            diamandManager = FindObjectOfType<DiamandManager>(); // Versuche ihn automatisch zu finden
        }
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal"); // Links-/Rechtsbewegung erfassen

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Neue Geschwindigkeit setzen

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer); // Bodenprüfung durchführen

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Springen, wenn Boden berührt und Taste gedrückt
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Y-Geschwindigkeit für Sprung setzen
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null) // Nur wenn GroundCheck gesetzt ist
        {
            Gizmos.color = Color.green; // Farbe der Box auf grün setzen
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize); // Bodenprüf-Bereich zeichnen
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin")) // Wenn Spieler ein Objekt mit "Coin"-Tag berührt
        {
            if (coinManager != null)
            {
                coinManager.AddCoin();
            }

            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Diamand")) // Wenn Spieler ein Objekt mit "Diamand"-Tag berührt
        {
            if (diamandManager != null)
            {
                diamandManager.AddDiamand();
            }

            Destroy(other.gameObject);
        }
    }
}
