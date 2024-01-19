using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image bar;

    public void UpdateHealth(int health, int maxHealth)
    {
        bar.fillAmount = (float)health / (float)maxHealth;
    }

    public void UpdateHealth(int oldhealth, int health, int maxHealth, float AnimTime)
    {
        float a = (float)health / (float)maxHealth;
        float b = (float)oldhealth/ (float)maxHealth;
        while (a != b)
        {
           b += Time.deltaTime * AnimTime;
           bar.fillAmount = b;
        }
        bar.fillAmount = a;
    }

}
