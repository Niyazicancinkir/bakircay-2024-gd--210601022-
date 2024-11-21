using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    public float liftHeight = 2.0f;
    public float moveSpeed = 5f; 

    private Vector3 snapTarget;
    private bool isSnapping = false;

    private Renderer objectRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();

        if (rb != null)
        {
            rb.useGravity = true;
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition) + offset;

            rb.velocity = (worldPosition - transform.position) * 10f;
        }
        else if (isSnapping)
        {
            float newYPosition = Mathf.Lerp(transform.position.y, snapTarget.y + liftHeight, Time.deltaTime * moveSpeed);
            Vector3 targetPosition = new Vector3(snapTarget.x, newYPosition, snapTarget.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(transform.position, targetPosition) < 0.7f)
            {
                isSnapping = false;
                StartCoroutine(WaitAndHide());
            }
            

        }
    }

    void OnMouseDown()
    {
        
            transform.position = new Vector3(transform.position.x, liftHeight, transform.position.z);
        

        isDragging = true;

        if (rb != null)
        {
            rb.useGravity = false;
            rb.velocity = Vector3.zero; 
        }

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.useGravity = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlacementArea"))
        {
            snapTarget = other.transform.position;
            isSnapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PlacementArea"))
        {
            isSnapping = false;
        }
    }

    void HideObject()
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Renderer bileþeni bulunamadý!");
        }
    }

    IEnumerator WaitAndHide()
    {
        yield return null;
        HideObject();
    }
}
