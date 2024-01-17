using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    ColoredFlash flash;
    EnemyAI enemyAI;

    private void Start()
    {
        flash = GetComponent<ColoredFlash>();
        enemyAI = GetComponent<EnemyAI>();
    }

    public void TakeDamEffect(int damage)
    {
        if (GameManager.Instance.damPopUp != null)
        {
            if(GameManager.Instance.PopupID != -1)
            {
                Transform instance = GameManager.Instance.pool.Get(GameManager.Instance.PopupID).transform;
                instance.position = transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Animator animator = instance.GetComponentInChildren<Animator>();
                if (damage <= 10) animator.Play("normal");
                else animator.Play("critical");
            }
            else
            {
                GameObject instance = Instantiate(GameManager.Instance.damPopUp, transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
                instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
                Animator animator = instance.GetComponentInChildren<Animator>();
                if (damage <= 10) animator.Play("normal");
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
}
