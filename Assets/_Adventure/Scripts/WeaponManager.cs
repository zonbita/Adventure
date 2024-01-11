using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public List<Weapon> Weapons;
    public List<Transform> Enemies = new List<Transform>();

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        GameManager.Instance.UplevelWeapon += UplevelWeapon;
        GameManager.Instance.GameRevive += Revive;
        InitWeapons();
    }

    private void Revive()
    {
        DestroyAllEnemies();
        Time.timeScale = 1;
    }

    private void InitWeapons()
    {
        Weapons = GetComponentsInChildren<Weapon>(true)
            .Where(weapon => weapon != null)
            .ToList();
    }

    private void UplevelWeapon(string s)
    {
        foreach (var weapon in Weapons)
        {
            if(weapon.data.itemType.ToString() == s && weapon.gameObject.activeSelf == false)
            {
                weapon.gameObject.SetActive(true);
                break;
            }
        }
    }
    public void DestroyAllEnemies()
    {
        StartCoroutine(DestroyEnemiesCoroutine());
    }

    private IEnumerator DestroyEnemiesCoroutine()
    {
        const int batchSize = 10;

        for (int i = 0; i < Enemies.Count; i += batchSize)
        {
            int endIndex = Mathf.Min(i + batchSize, Enemies.Count);

            for (int j = i; j < endIndex; j++)
            {
                Transform enemy = Enemies[j];

                if (enemy != null)
                {
                    Destroy(enemy.gameObject);
                }
            }

            yield return null;
        }

        Enemies.Clear();
    }

    public void AddEnemyToFireRange(Transform transform)
    {
        Enemies.Add(transform);
    }

    public void RemoveEnemyToFireRange(Transform transform)
    {
        Enemies.Remove(transform);
    }


    void CalculatorNearestEnemy()
    {
        if (Enemies.Count < 2) return;

        for (int i = 0; i < Enemies.Count; i++)
        {
            for (int j = 1; j < Enemies.Count; j++)
            {
                if (Vector2.Distance(Enemies[j].position, transform.position) < Vector2.Distance(Enemies[i].position, transform.position))
                    Enemies[i] = Enemies[j];
            }
        }
    }
    public Transform FindEnemyWithIndex(int Index)
    {
        if (Enemies[Index] != null)
        {
            return Enemies[Index];
        }

        return Enemies[0];
    }
    public Transform FindNearestEnemy(Vector2 weaponPos)
    {
        if (Enemies != null && Enemies.Count <= 0) return null;
        Transform nearestEnemy = Enemies[0];
        foreach (Transform enemy in Enemies)
        {
            if (Vector2.Distance(enemy.position, weaponPos) < Vector2.Distance(nearestEnemy.position, weaponPos))
                nearestEnemy = enemy;
        }

        return nearestEnemy;
    }


}

