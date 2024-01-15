using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public GameObject[] prefabs;

    public List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }

        return select;
    }

    public int GetID(GameObject go)
    {
        for (int index = 0; index < prefabs.Length; index++)
        {
            if (go == prefabs[index])
            {
                return index;
            }
        }
        print("Please, Add at Pool Manager");
        return -1;
    }

    public void SpawnWait(int prefabId, int max)
    {
        int i = 0;
        int j = pools[prefabId].Count;
        while (j < max)
        {
            GameObject go = Instantiate(prefabs[prefabId], transform);
            go.SetActive(false);
            pools[prefabId].Add(go);

            j++;
        }
    }

    public Transform GetGameobjectDeactive(int index)
    {
        if (pools[index].Count != 0)
        {
            foreach (GameObject item in pools[index])
            {
                if(!item.activeSelf)
                    return item.transform;
            }
        }
        print("Please, Add at Pool Manager");
        return null;
    }
}
