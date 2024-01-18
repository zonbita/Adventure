using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour, I_Damage
{
    ColoredFlash flash;
    EnemyAI enemyAI;

    private void Start()
    {
        flash = GetComponent<ColoredFlash>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamageEffect(int damage, int max)
    {
        if (GameManager.Instance.damPopUp != null)
        {
            if (GameManager.Instance.PopupID != -1)
            {
                Transform instance = GameManager.Instance.pool.Get(GameManager.Instance.PopupID).transform;
                instance.position = transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0);
                string dmg = damage.ToString();
                
                Animator animator = instance.GetComponentInChildren<Animator>();
                if (damage >= max - 1) {
                    dmg = "Crits " + dmg;
                    animator.Play("red");
                    
                } 
                else if (damage <= max - 4) animator.Play("normal");
                else animator.Play("critical");
                instance.GetComponentInChildren<TextMeshProUGUI>().text = dmg;
            }
            else
            {
                GameObject instance = Instantiate(GameManager.Instance.damPopUp, transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Animator animator = instance.GetComponentInChildren<Animator>();
                if (damage >= max) animator.Play("red");
                else if (damage <= max - 5) animator.Play("normal");
                else animator.Play("critical");
            }

        }

        // Flash
        if (flash != null)
        {
            flash.Flash(Color.white);
        }
        // Freeze
        if (enemyAI != null)
        {
            enemyAI.FreezeEnemy();
        }
    }

    public void TakeDamage(int damage)
    {
        
    }
}
