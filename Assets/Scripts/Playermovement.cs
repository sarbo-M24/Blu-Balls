using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Ball Settings")]
    [SerializeField] private float speed = 8f;

    [Header("Steering")]
    [SerializeField] private float steerStrength = 45f;   // degrees per second the player can rotate direction
    [SerializeField] private float maxSteerAngle = 60f;   // max degrees the player can deviate from the "natural" bounce direction

    private Rigidbody rb;
    private Vector3 direction;
    private Vector3 naturalDirection; // direction as physics would have it, without player bias

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotation;
    }

    private void Start()
    {
        direction = new Vector3(1f, 1f, 0f).normalized;
        naturalDirection = direction;
        rb.linearVelocity = direction * speed;
    }

    private void Update()
    {
        // Read WASD input — only X and Y axes matter (Z frozen)
        float h = 0f, v = 0f;

        if (Input.GetKey(KeyCode.A)) h = -1f;
        else if (Input.GetKey(KeyCode.D)) h = 1f;

        if (Input.GetKey(KeyCode.S)) v = -1f;
        else if (Input.GetKey(KeyCode.W)) v = 1f;

        if (h == 0f && v == 0f) return;

        // Build a desired nudge direction from input
        Vector3 inputDir = new Vector3(h, v, 0f).normalized;

        // Rotate current direction toward the nudged result
        Vector3 targetDir = Vector3.RotateTowards(
            direction,
            inputDir,
            steerStrength * Mathf.Deg2Rad * Time.deltaTime,
            0f
        );

        // Clamp: don't let player steer more than maxSteerAngle away from natural bounce direction
        float angleFromNatural = Vector3.Angle(naturalDirection, targetDir);
        if (angleFromNatural <= maxSteerAngle)
        {
            direction = targetDir.normalized;
            rb.linearVelocity = direction * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("walls")) return;

        Vector3 normal = collision.contacts[0].normal;

        // Reflect both tracked directions
        direction = Vector3.Reflect(direction, normal).normalized;
        naturalDirection = Vector3.Reflect(naturalDirection, normal).normalized;

        transform.position += normal * 0.05f;
        rb.linearVelocity = direction * speed;
    }

    public void Launch(Vector3 launchDirection)
    {
        direction = launchDirection.normalized;
        naturalDirection = direction;
        rb.linearVelocity = direction * speed;
    }
}