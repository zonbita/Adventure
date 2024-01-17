using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SerializableBossLevel
{
    public int Level;
    public Boss_Base Boss_Object;
}

public class SpawnerManager : Singleton<SpawnerManager>
{
    public Action<int, Boss_Base> boss;

    [SerializeField] private List<SerializableBossLevel> BossList = new List<SerializableBossLevel>();
    private Dictionary<int, Boss_Base> _BossList;

    [Header("Options")]
    public WeaponManager _weaponManager;
    public int maxEnemy = 5;
    public float startTimeBtwSpawn;

    private float timeBtwSpawn = 2;
    private Player player;
    int roundCount = 0;
    List<Spawner> _SpawnPoints;

    void Awake()
    {
        // Init
        _BossList = new Dictionary<int, Boss_Base>();
        foreach (var v in BossList)
        {
            _BossList[v.Level] = v.Boss_Object;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        _SpawnPoints = new List<Spawner>(GetComponentsInChildren<Spawner>());
        GameManager.Instance.UpLevel += UpLevel;
    }

    private void UpLevel()
    {
        int level = GameManager.Instance.Level;
        if (_BossList.ContainsKey(level))
        {
            SpawnBoss(level);
        }
    }

    public void SpawnBoss(int level)
    {
        GameObject go = Instantiate(_BossList[level], _SpawnPoints[UnityEngine.Random.Range(0, _SpawnPoints.Count())].transform.position, Quaternion.identity).gameObject;
        WeaponManager.instance.AddEnemyToFireRange(go.transform);
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
