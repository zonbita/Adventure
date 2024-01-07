using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float startTimeBtwSpawn = 2;
    public List<GameObject> enemies;

    private void Start()
    {
        GameManager.Instance.AddMonster += AddMonster;
    }

    private void AddMonster(GameObject obj)
    {
        enemies.Add(obj);
    }

    public void spawnEnemy(Vector3 location)
    {
        if (enemies.Count == 0) return;

        GameObject go = Instantiate(enemies[Random.Range(0,enemies.Count)], location, Quaternion.identity);
        WeaponManager.instance.AddEnemyToFireRange(go.transform);
    }
}
