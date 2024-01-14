using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exp : MonoBehaviour
{
    public int xp = 1;
    SpriteRenderer s;


    public int XP
    {
        get { return xp; }
        set { xp = value;
            Init();
        }
    }
    private void OnEnable()
    {
        Init();


    }
    private void Init()
    {
        s = GetComponent<SpriteRenderer>();
        Sprite s2 = ExpManager.Instance.GetSprite(xp);
        if (s2 != null)
            s.sprite = s2;
        
    }
    public void Up_Exp()
    {
        GameManager.Instance.CurrentExp += xp;
    }
}
