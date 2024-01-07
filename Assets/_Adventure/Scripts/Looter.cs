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
            //collision.enabled = false;
            collision.gameObject.GetComponent<Follow>().Reset();
            //collision.gameObject.GetComponent<CoinAction>().enabled = true;
            AudioManager.instance?.PlaySfx(AudioManager.Sfx.Loot);
        }
    }

}
