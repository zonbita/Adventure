using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[ExecuteInEditMode]
public class ExpManager : Singleton<ExpManager>
{
    public Sprite[] Sprites;
    public int[] XPList;

    private void Awake()
    {
        XPList = new int[Sprites.Length];
        for (int i = 0; i < XPList.Length; i++)
        {
            XPList[i] = 1+i;
        }
    }
    public Sprite GetSprite(int xp)
    {
        int index = Array.FindIndex(XPList, element => element == xp);

        if (index != -1)
        {

            return Sprites[index];
        }
        return null;
    }
}
