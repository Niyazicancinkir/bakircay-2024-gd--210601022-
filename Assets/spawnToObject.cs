using System.Diagnostics;
using System;
using UnityEngine;

public class spawnToObject : MonoBehaviour
{
    public GameObject objectToSpawn;  // Spawn etmek istediðiniz obje
    private Transform playgroundArea; // Playground area'nýn transform'u

    void Start()
    {
        // Tag ile PlaygroundArea objesini bul
        playgroundArea = GameObject.FindGameObjectWithTag("playGroundArea").transform;

        if (playgroundArea == null)
        {
            UnityEngine.Debug.LogError("PlayGroundArea tag'ine sahip bir obje bulunamadý!");
            return;
        }

        // Objeleri spawn et
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Playground alanýnýn boyutunu almak
        float xPos = UnityEngine.Random.Range(-playgroundArea.localScale.x / 2, playgroundArea.localScale.x / 2);
        float zPos = UnityEngine.Random.Range(-playgroundArea.localScale.z / 2, playgroundArea.localScale.z / 2);
        Vector3 spawnPosition = new Vector3(xPos, 0, zPos);

        // Objeyi spawn etme iþlemi
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Spawn edilen objeye tag atýyoruz
        spawnedObject.tag = "spawnedObject";  // Eðer objenin tag'ý önceden atanmadýysa
    }

    void Update()
    {
        // Spawn edilen objeyi tag ile bulmak (örnek)
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        foreach (GameObject obj in spawnedObjects)
        {
            // Bu nesne üzerinde iþlemler yapabilirsiniz (örneðin, hareket, renk deðiþtirme vs.)
            UnityEngine.Debug.Log("Spawned object: " + obj.name);
        }
    }
}
