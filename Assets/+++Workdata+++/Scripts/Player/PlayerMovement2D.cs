using UnityEngine; // Zugriff auf Unity-spezifische Klassen und Methoden

public class PlayerMovement2D : MonoBehaviour // Klasse für die 2D-Spielerbewegung
{
    public float moveSpeed = 5f; // Bewegungsgeschwindigkeit des Spielers
    public float jumpForce = 10f; // Sprungkraft nach oben

    public Transform groundCheck; // Transform-Objekt zur Bodenprüfung
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.1f); // Größe des Bodenprüfungsbereichs

    public LayerMask groundLayer; // Layer-Maske für den Boden
    public CoinManager coinManager; // Referenz auf den CoinManager

    public DiamandManager diamandManager; // Referenz auf den DiamandManager
    private Rigidbody2D rb; // Rigidbody-Komponente für die Physik

    private bool isGrounded; // Ob der Spieler aktuell auf dem Boden ist
    private bool canMove = true; // Steuerung, ob der Spieler sich bewegen darf

    void Start() // Start-Methode, wird beim Spielstart aufgerufen
    {
        rb = GetComponent<Rigidbody2D>(); // Zugriff auf Rigidbody-Komponente

        if (coinManager == null) // Falls kein CoinManager zugewiesen wurde
        {
            coinManager = FindObjectOfType<CoinManager>(); // Automatisches Suchen des CoinManagers
        }

        if (diamandManager == null) // Falls kein DiamandManager zugewiesen wurde
        {
            diamandManager = FindObjectOfType<DiamandManager>(); // Automatisches Suchen des DiamandManagers
        }
    }

    void Update() // Wird jede Frame aufgerufen
    {
        if (!canMove) return; // Wenn Bewegung deaktiviert, verlasse Update()

        float moveInput = Input.GetAxisRaw("Horizontal"); // Eingabe für horizontale Bewegung

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y); // Geschwindigkeit anpassen

        isGrounded = Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer); // Prüft, ob Boden berührt wird

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // Wenn Leertaste gedrückt und Spieler am Boden
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Sprungkraft nach oben setzen
        }
    }

    public void SetCanMove(bool value) // Aktiviert oder deaktiviert Spielerbewegung
    {
        canMove = value; // Setzt den Bewegungsstatus
    }

    void OnDrawGizmosSelected() // Zeichnet Hilfslinien im Editor
    {
        if (groundCheck != null) // Nur wenn ein GroundCheck-Objekt gesetzt ist
        {
            Gizmos.color = Color.green; // Farbe für die Bodenprüfung auf grün setzen
            Gizmos.DrawWireCube(groundCheck.position, groundCheckSize); // Zeichnet die Bodenprüfbox
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Wird aufgerufen, wenn Spieler mit Trigger kollidiert
    {
        if (other.CompareTag("Coin")) // Prüft, ob ein Coin berührt wurde
        {
            if (coinManager != null) // Falls CoinManager vorhanden ist
            {
                coinManager.AddCoin(); // Eine Münze hinzufügen
            }

            Destroy(other.gameObject); // Coin-Objekt zerstören
        }
        else if (other.CompareTag("Diamand")) // Prüft, ob ein Diamand berührt wurde
        {
            if (diamandManager != null) // Falls DiamandManager vorhanden ist
            {
                diamandManager.AddDiamand(); // Einen Diamanten hinzufügen
            }

            Destroy(other.gameObject); // Diamant-Objekt zerstören
        }
    }
}
