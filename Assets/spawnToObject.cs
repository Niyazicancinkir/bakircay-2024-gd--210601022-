using System.Diagnostics;
using System;
using UnityEngine;

public class spawnToObject : MonoBehaviour
{
    public GameObject objectToSpawn;  // Spawn etmek istedi�iniz obje
    private Transform playgroundArea; // Playground area'n�n transform'u

    void Start()
    {
        // Tag ile PlaygroundArea objesini bul
        playgroundArea = GameObject.FindGameObjectWithTag("playGroundArea").transform;

        if (playgroundArea == null)
        {
            UnityEngine.Debug.LogError("PlayGroundArea tag'ine sahip bir obje bulunamad�!");
            return;
        }

        // Objeleri spawn et
        SpawnObjects();
    }

    void SpawnObjects()
    {
        // Playground alan�n�n boyutunu almak
        float xPos = UnityEngine.Random.Range(-playgroundArea.localScale.x / 2, playgroundArea.localScale.x / 2);
        float zPos = UnityEngine.Random.Range(-playgroundArea.localScale.z / 2, playgroundArea.localScale.z / 2);
        Vector3 spawnPosition = new Vector3(xPos, 0, zPos);

        // Objeyi spawn etme i�lemi
        GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

        // Spawn edilen objeye tag at�yoruz
        spawnedObject.tag = "spawnedObject";  // E�er objenin tag'� �nceden atanmad�ysa
    }

    void Update()
    {
        // Spawn edilen objeyi tag ile bulmak (�rnek)
        GameObject[] spawnedObjects = GameObject.FindGameObjectsWithTag("spawnedObject");

        foreach (GameObject obj in spawnedObjects)
        {
            // Bu nesne �zerinde i�lemler yapabilirsiniz (�rne�in, hareket, renk de�i�tirme vs.)
            UnityEngine.Debug.Log("Spawned object: " + obj.name);
        }
    }
}
