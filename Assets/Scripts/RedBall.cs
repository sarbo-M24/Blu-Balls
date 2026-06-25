using System.Collections;
using UnityEngine;

public class RedBall : MonoBehaviour
{
    public delegate void RedballCollision();

    public static event RedballCollision OnRedballCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            Debug.Log("Red ball collided with line!");
            OnRedballCollision?.Invoke();
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return null;
        Destroy(gameObject);
    }
}
