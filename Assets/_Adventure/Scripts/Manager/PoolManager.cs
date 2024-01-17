using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public GameObject[] prefabs;

    [SerializeField] List<GameObject>[] pools;

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
        int id = -1;
  
        for (int index = 0; index < prefabs.Length; index++)
        {
            if (go == prefabs[index])
            {
                return index;
            }
        }



        Debug.LogWarning("Please add prefab " + go + "to pooling");

        return id;
    }

    public void SpawnWait(int prefabId, int max)
    {
        int m = pools[prefabId].Count;
        while (m < max)
        {
            pools[prefabId].Add(CreateGobject(prefabs[prefabId]));
            m++;
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
    GameObject CreateGobject(GameObject item)
    {
        GameObject gobject = Instantiate(item, transform);
        gobject.SetActive(false);
        return gobject;
    }
}
