using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float speed = 8f;

    private Rigidbody rb;
    private Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Disable gravity — ball moves in the XY plane
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Freeze Z position and X/Y rotation so the ball stays in the XY plane
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        // Launch the ball at a 45-degree angle to start
        direction = new Vector3(1f, 1f, 0f).normalized;
        rb.linearVelocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Only bounce off objects tagged "Walls"
        if (!collision.gameObject.CompareTag("walls")) return;

        // Get the collision normal to calculate the bounce direction
        Vector3 normal = collision.contacts[0].normal;

        // Reflect the current direction off the surface normal
        direction = Vector3.Reflect(direction, normal).normalized;

        // Nudge the ball away from the surface to prevent sticking
        transform.position += normal * 0.05f;

        // Re-apply velocity to maintain consistent speed after bounce
        rb.linearVelocity = direction * speed;
    }

    // Call this from a launcher or UI to reset + re-launch
    public void Launch(Vector3 launchDirection)
    {
        direction = launchDirection.normalized;
        rb.linearVelocity = direction * speed;
    }
}