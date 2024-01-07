using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;

    public void UpdateHealth(int health, int maxHealth)
    {
        bar.fillAmount = (float)health / (float)maxHealth;
    }

}
