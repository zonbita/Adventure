using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class RandomCard : MonoBehaviour 
{
    Dictionary<ItemData.ItemType, int> WeaponUpLevel = new Dictionary<ItemData.ItemType, int>();
    public List<ItemData> itemDatas;
    private void Start()
    {
        WeaponUpLevel.Add(ItemData.ItemType.Default, 1);
        StartRandomCard();
    }

    void StartRandomCard()
    {
        ItemData.ItemType[] allItemTypes = (ItemData.ItemType[])Enum.GetValues(typeof(ItemData.ItemType));

        System.Random random = new System.Random();
        ItemData.ItemType[] randomItemTypes = allItemTypes.OrderBy(_ => random.Next()).Take(3).ToArray();


        foreach (var itemType in randomItemTypes)
        {
            int index = Array.IndexOf(Enum.GetValues(typeof(ItemData.ItemType)), itemType);
            print(index);
        }
    }
}
