using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor.Timeline.Actions;
using Unity.VisualScripting;

public class Coin : MonoBehaviour
{
    public int MinNumber = 1;
    public int MaxNumber = 3;
    int value = 1;
    private Collider2D co;
    public void Awake()
    { 
        co = GetComponent<Collider2D>();
    }
    private void Start()
    {
        value = UnityEngine.Random.Range(MinNumber, MaxNumber);
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Drop);
    }
    private void OnEnable()
    {
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Drop);
        co.enabled = true;
    }
}
