using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemDrop : MonoBehaviour
{
    public ItemData data;

    BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if(c.CompareTag("Player"))
        {
            if (c.enabled)
            {
                
                c.enabled = false;
                switch (data.itemName)
                {
                    case "Magnet":
                        GameManager.Instance?.HarvertAllCoin();
                        break;
                    case "Healing":
                        GameManager.Instance?.Player.health.Healing(data.baseDamage);
                        break;
                }
                Destroy(this.gameObject);

            }

        }
    }
}
