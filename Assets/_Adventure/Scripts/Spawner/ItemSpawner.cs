using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;
    [Serializable]
    public class SpawnableItem
    {
        public GameObject itemPrefab;
        public float spawnChance;
    }

    public List<SpawnableItem> spawnableItems;
    public float SpawningTime = 30f;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine("SpawnRandomItem", SpawningTime); 
    }

    IEnumerable SpawnRandomItem()
    {
        // Generate a random number between 0 and 1
        float randomValue = UnityEngine.Random.value;

        // choose item
        float Weight = 0f;
        foreach (var spawnableItem in spawnableItems)
        {
            Weight += spawnableItem.spawnChance / 100f;

            if (randomValue <= Weight)
            {
                Instantiate(spawnableItem.itemPrefab, transform.position, Quaternion.identity);
                Debug.Log("Spawned: " + spawnableItem.itemPrefab.name);
                yield return null;
            }
        }
        yield return null;
    }
}