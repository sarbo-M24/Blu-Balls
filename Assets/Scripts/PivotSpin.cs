using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PivotSpin : MonoBehaviour
{
    [Header("Spin Settings")]
    [SerializeField] private float spinSpeed = 180f; // degrees per second

    private bool isClockwise = false; // starts anticlockwise

    public GameObject panel;
    private void Update()
    {
        // Detect tap (touch or mouse for editor testing)
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            isClockwise = !isClockwise;
            StartCoroutine(panelActivation());
            Debug.Log("Tapped! Now spinning " + (isClockwise ? "clockwise" : "anticlockwise"));
        }

        // Anticlockwise = positive Z rotation, Clockwise = negative Z rotation
        float rotationDir = isClockwise ? -1f : 1f;
        transform.Rotate(0f, 0f, rotationDir * spinSpeed * Time.deltaTime);
    }

    IEnumerator panelActivation() {
        panel.gameObject.SetActive(true);

        yield return new WaitForSeconds(.6f);

        panel.gameObject.SetActive(false);
    }
}