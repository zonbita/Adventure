using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Explosion_Radius = 1;
    public Vector2 direction;
    void StartExplosion(Vector3 p)
    {
        Collider2D[] co = Physics2D.OverlapCircleAll(p, Explosion_Radius);
        foreach(Collider2D c in co)
        {
            if (c.CompareTag("Player"))
            {
                c.gameObject.GetComponent<I_Damage>()?.TakeDamageEffect(2, 5);
                break;
            }
                
        }
        gameObject.SetActive(false);
    }
}
