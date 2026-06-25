using System.Collections;
using UnityEngine;

public class bluBall : MonoBehaviour
{

    public delegate void BlueballCollision();

    public static event BlueballCollision OnBlueballCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Line"))
        {
            Debug.Log("Blue ball collided with line!");
            OnBlueballCollision?.Invoke();
            StartCoroutine(DeactivateAfterDelay());
        }
    }

    IEnumerator DeactivateAfterDelay()
    {
        yield return null;
        Destroy(gameObject);
    }
}


// destroyable Objects : interface
   // Box - unique logic 
   // Red barrel
   // xyz