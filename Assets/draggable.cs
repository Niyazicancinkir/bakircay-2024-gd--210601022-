using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    public float liftHeight = 1.0f; // Yukar� kald�rma miktar�
    public float moveSpeed = 5f; // Hareket h�z�

    private Vector3 snapTarget;
    private bool isSnapping = false;

    private Renderer objectRenderer; // Nesnenin Renderer bile�eni

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>(); // Nesnenin Renderer bile�enini al

        if (rb != null)
        {
            rb.isKinematic = true; // Ba�lang��ta kinematik yap�yoruz.
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = worldPosition + offset;
        }
        else if (isSnapping)
        {
            // Yumu�ak ge�i� ile yerle�tirme alan�n�n merkezine do�ru hareket
            transform.position = Vector3.Lerp(transform.position, snapTarget, Time.deltaTime * moveSpeed);

            // Hedef pozisyona �ok yak�nsa snapping'i durdur
            if (Vector3.Distance(transform.position, snapTarget) < 0.5f)
            {
                isSnapping = false;
                StartCoroutine(WaitAndHide()); // 1 saniye bekle, sonra nesneyi gizle
            }
        }
    }

    void OnMouseDown()
    {
        isDragging = true;

        if (rb != null)
        {
            rb.isKinematic = true; // Yer�ekimini durdurmak i�in kinematik yap�yoruz.
        }

        transform.position += new Vector3(0, liftHeight, 0); // Nesneyi yukar� kald�r�yoruz

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.isKinematic = false; // Yer�ekimini tekrar aktifle�tiriyoruz.
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Yerle�tirme alan� ile �arp��ma
        if (other.CompareTag("PlacementArea"))
        {
            // Yerle�tirme alan�n�n merkezine do�ru hareket etmesini sa�la
            snapTarget = other.transform.position;
            isSnapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // E�er nesne yerle�tirme alan�ndan ��karsa snapping'i durdur
        if (other.CompareTag("PlacementArea"))
        {
            isSnapping = false;
        }
    }

    // Nesnenin g�r�n�rl���n� kapatma fonksiyonu
    void HideObject()
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false; // Renderer bile�eninin aktifli�ini kapat
        }
    }

    // 1 saniye bekleme i�lemi
    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        HideObject(); // Bekleme s�resi bittikten sonra nesneyi gizle
    }
}
