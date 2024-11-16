using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 offset;
    private Rigidbody rb;

    public float liftHeight = 1.0f; // Yukarý kaldýrma miktarý
    public float moveSpeed = 5f; // Hareket hýzý

    private Vector3 snapTarget;
    private bool isSnapping = false;

    private Renderer objectRenderer; // Nesnenin Renderer bileþeni

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>(); // Nesnenin Renderer bileþenini al

        if (rb != null)
        {
            rb.isKinematic = true; // Baþlangýçta kinematik yapýyoruz.
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
            // Yumuþak geçiþ ile yerleþtirme alanýnýn merkezine doðru hareket
            transform.position = Vector3.Lerp(transform.position, snapTarget, Time.deltaTime * moveSpeed);

            // Hedef pozisyona çok yakýnsa snapping'i durdur
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
            rb.isKinematic = true; // Yerçekimini durdurmak için kinematik yapýyoruz.
        }

        transform.position += new Vector3(0, liftHeight, 0); // Nesneyi yukarý kaldýrýyoruz

        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;
        offset = transform.position - Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (rb != null)
        {
            rb.isKinematic = false; // Yerçekimini tekrar aktifleþtiriyoruz.
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Yerleþtirme alaný ile çarpýþma
        if (other.CompareTag("PlacementArea"))
        {
            // Yerleþtirme alanýnýn merkezine doðru hareket etmesini saðla
            snapTarget = other.transform.position;
            isSnapping = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Eðer nesne yerleþtirme alanýndan çýkarsa snapping'i durdur
        if (other.CompareTag("PlacementArea"))
        {
            isSnapping = false;
        }
    }

    // Nesnenin görünürlüðünü kapatma fonksiyonu
    void HideObject()
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = false; // Renderer bileþeninin aktifliðini kapat
        }
    }

    // 1 saniye bekleme iþlemi
    IEnumerator WaitAndHide()
    {
        yield return new WaitForSeconds(1f); // 1 saniye bekle
        HideObject(); // Bekleme süresi bittikten sonra nesneyi gizle
    }
}
