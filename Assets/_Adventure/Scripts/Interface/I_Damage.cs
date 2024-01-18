using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Damage
{
    public void TakeDamage(int damage);
    public void TakeDamageEffect(int damage, int max);
}
