using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Bewegungsziele")]
    public Transform pointA;
    public Transform pointB;

    [Header("Bewegungseinstellungen")]
    public float speed = 2f;

    private Vector3 target;
    private bool isMoving = false;

    private void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError(" PointA oder PointB ist nicht zugewiesen!");
            enabled = false;
            return;
        }

        target = pointB.position;
        isMoving = false; // Plattform startet NICHT automatisch
    }

    private void Update()
    {
        if (!isMoving) return;

        MovePlatform();
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }
}