using UnityEngine;

/// <summary>
/// Makes this object track a target's forward-axis position.
/// Put on side walls, the spawner, and the camera in Endless mode.
/// </summary>
public class ForwardFollower : MonoBehaviour
{
    public enum Axis { X, Y, Z }

    [Header("Follow Target")]
    [SerializeField] private Transform target;          // usually the player ball
    [SerializeField] private Axis forwardAxis = Axis.Y;  // matches XY-plane setup

    [Tooltip("Keep the initial offset between this object and the target.")]
    [SerializeField] private bool preserveStartOffset = true;

    [Tooltip("0 = snap instantly, >0 = smooth follow time.")]
    [SerializeField] private float smoothTime = 0f;

    private float offset;
    private float velocity; // for SmoothDamp

    private void Start()
    {
        if (target == null) return;
        offset = preserveStartOffset
            ? AxisValue(transform.position) - AxisValue(target.position)
            : 0f;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        float desired = AxisValue(target.position) + offset;
        float current = AxisValue(transform.position);
        float next = smoothTime > 0f
            ? Mathf.SmoothDamp(current, desired, ref velocity, smoothTime)
            : desired;

        Vector3 p = transform.position;
        switch (forwardAxis)
        {
            case Axis.X: p.x = next; break;
            case Axis.Y: p.y = next; break;
            case Axis.Z: p.z = next; break;
        }
        transform.position = p;
    }

    private float AxisValue(Vector3 v) =>
        forwardAxis == Axis.X ? v.x : forwardAxis == Axis.Y ? v.y : v.z;
}