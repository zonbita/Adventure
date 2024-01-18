using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Pipeline;
using UnityEngine;

public class Boss_1 : Boss_Base
{
    I_Skill i_Skill;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        i_Skill = GetComponent<I_Skill>();
        StartCoroutine("StartSkill", 5.0f);
    }

    IEnumerator StartSkill(float StartDelay)
    {
        while (true)
        {
/*            yield return new WaitForSeconds(StartDelay);
            StopMovement();
            i_Skill.Active("Projectile");

            yield return new WaitForSeconds(2.0f);
            EnableMovement();*/
            yield return new WaitForSeconds(3);
            i_Skill.Active("Teleport");
            yield return new WaitForSeconds(StartDelay);
/*            StopMovement();
            i_Skill.Active("ShootMultiDirection");
            yield return new WaitForSeconds(2.0f);
            EnableMovement();
            yield return new WaitForSeconds(2.0f);*/
        }

    }
}
