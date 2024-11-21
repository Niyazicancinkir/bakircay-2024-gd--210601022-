using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnToPrefab : MonoBehaviour
{
    public GameObject[] objectsToSpawn;  
    private Transform playgroundArea;   
    public float spawnHeight = 5f;     

    void Start()
    {
        SpawnObjects();

    }

    void SpawnObjects()
    {
        if (objectsToSpawn == null || objectsToSpawn.Length == 0)
        {
            UnityEngine.Debug.LogError("objectsToSpawn listesi boþ! Lütfen Inspector'da prefab'lar ekleyin.");
            return;
        }

        foreach (GameObject prefab in objectsToSpawn)
        {
            if (prefab == null)
            {
                UnityEngine.Debug.LogError("Listede null bir prefab bulundu! Lütfen kontrol edin.");
                continue;
            }

            Vector3 spawnPosition1 = GetRandomSpawnPosition();
            GameObject spawnedObject1 = Instantiate(prefab, spawnPosition1, Quaternion.identity);
            ConfigureRigidbody(spawnedObject1);

            Vector3 spawnPosition2 = GetRandomSpawnPosition();
            GameObject spawnedObject2 = Instantiate(prefab, spawnPosition2, Quaternion.identity);
            ConfigureRigidbody(spawnedObject2);
        }
    }

    Vector3 GetRandomSpawnPosition()
    {
        float xPos = UnityEngine.Random.Range(-5f, 5f);
        float zPos = UnityEngine.Random.Range(-3f, 5f);
        return new Vector3(xPos, spawnHeight, zPos); 
    }

    void ConfigureRigidbody(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>(); 
        }

        rb.useGravity = true;
        rb.isKinematic = false; 
    }
}
