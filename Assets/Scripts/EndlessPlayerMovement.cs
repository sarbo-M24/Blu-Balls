using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EndlessPlayerMovement : MonoBehaviour
{
    [Header("Forward Motion")]
    [SerializeField] private float forwardSpeed = 8f;   // constant +Y travel
    [SerializeField] private float lateralSpeed = 8f;   // side-to-side speed

    [Header("Start Direction")]
    [Tooltip("Initial lateral direction: +1 right, -1 left.")]
    [SerializeField] private float startLateralSign = 1f;

    private Rigidbody rb;
    private float lateralDir; // -1 or +1, flips on wall bounce

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
        lateralDir = Mathf.Sign(startLateralSign == 0 ? 1f : startLateralSign);
        ApplyVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Side walls flip lateral direction; forward speed is untouched → 45° feel preserved
        if (collision.gameObject.CompareTag("walls"))
        {
            Vector3 normal = collision.contacts[0].normal;
            // Flip lateral direction based on which wall we hit
            lateralDir = Mathf.Sign(normal.x != 0 ? normal.x : -lateralDir);
            transform.position += new Vector3(normal.x, 0f, 0f) * 0.05f;
            ApplyVelocity();
        }
    }

    private void ApplyVelocity()
    {
        rb.linearVelocity = new Vector3(lateralDir * lateralSpeed, forwardSpeed, 0f);
    }

    // For a reset/launcher
    public void Relaunch(float newLateralSign)
    {
        lateralDir = Mathf.Sign(newLateralSign == 0 ? 1f : newLateralSign);
        ApplyVelocity();
    }
}