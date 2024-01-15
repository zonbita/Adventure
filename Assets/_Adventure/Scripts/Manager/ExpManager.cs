using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteInEditMode]
public class ExpManager : Singleton<ExpManager>
{
    public SpriteAtlas atlas;
    public string[] names;
    public int[] XPList;

    private void Awake()
    {

        XPList = new int[names.Length];
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

            return atlas.GetSprite(names[index]);
        }
        return null;
    }
}
