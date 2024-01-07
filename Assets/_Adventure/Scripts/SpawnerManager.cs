using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public static SpawnerManager instance;

    public float startTimeBtwSpawn;
    private float timeBtwSpawn = 2;

    public WeaponManager _weaponManager;
    List<Spawner> _SpawnPoints;


    private Player player;
    public int maxEnemy = 5;
    int roundCount = 0;


    private void Awake() => instance = this;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        _SpawnPoints = new List<Spawner>(GetComponentsInChildren<Spawner>());
    }

    public List<int> GetRandomIndices(int n, int k)
    {

        // Create a list containing all indices from 0 to n-1
        List<int> allIndices = new List<int>();
        for (int i = 0; i < n; i++)
        {
            allIndices.Add(i);
        }

        // Create a list to store the randomly selected indices
        List<int> randomIndices = new List<int>();

        // Use Fisher-Yates shuffle algorithm to randomly shuffle the indices
        int remainingItems = n;
        for (int i = 0; i < k; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, remainingItems);
            randomIndices.Add(allIndices[randomIndex]);
            // Move the last index in the list to the current position
            allIndices[randomIndex] = allIndices[remainingItems - 1];
            remainingItems--;
        }

        return randomIndices;
    }

    private void Update()
    {
        if (timeBtwSpawn <= 0)
        {
            int rand = UnityEngine.Random.Range(2, maxEnemy);

            // Enemy < 5
            if (_weaponManager.Enemies.Count <= 5)    rand = UnityEngine.Random.Range(maxEnemy - 2, maxEnemy);

            List<int> randomIndex = GetRandomIndices(maxEnemy, rand);
            foreach (int index in randomIndex)
            {
                if (index >= 0 && index < _SpawnPoints.Count && _SpawnPoints[index] != null)
                {
                    _SpawnPoints[index].spawnEnemy(_SpawnPoints[index].transform.position);
                }
            }
               
            

            timeBtwSpawn = startTimeBtwSpawn;

            roundCount++;
            if (roundCount > 10)
            {
                roundCount = 0;
                maxEnemy = Mathf.Max(_SpawnPoints.Count, maxEnemy + 1);
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
    }
}
