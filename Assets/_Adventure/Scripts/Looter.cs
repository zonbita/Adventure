using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Looter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            collision.gameObject.GetComponent<Follow>().Reset();
            AudioManager.instance?.PlaySfx(AudioManager.Sfx.Loot);
        }

        if (collision.CompareTag("Item"))
        {
            ItemData data = collision.gameObject.GetComponent<ItemDrop>().data;
            if (collision.enabled && data)
            {
                collision.enabled = false;
                switch (data.itemName)
                {
                    case "Magnet":
                        GameManager.Instance?.HarvertAllCoin();
                        
                        break;
                    case "Healing":
                        GameManager.Instance?.Player.health.Healing(data.baseDamage);
                        break;
                }
                // Deactivate or destroy the item
                collision.gameObject.SetActive(false);
                AudioManager.instance?.PlaySfx(AudioManager.Sfx.Loot);
            }
           

        }
    }

}
