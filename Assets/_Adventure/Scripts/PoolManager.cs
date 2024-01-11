using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;
    public GameObject[] prefabs;

    public List<GameObject>[] pools;

    void Awake()
    {
        instance = this;
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
}
