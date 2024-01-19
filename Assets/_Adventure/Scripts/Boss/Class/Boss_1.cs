using System.Collections;
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
            yield return new WaitForSeconds(StartDelay);
            StopMovement();
            RotateGun(target.position);
            i_Skill.Active("Projectile");

            yield return new WaitForSeconds(2.0f);
            EnableMovement();
            yield return new WaitForSeconds(3);
            i_Skill.Active("Teleport");
            yield return new WaitForSeconds(StartDelay);
            StopMovement();
            RotateGun(target.position);
            i_Skill.Active("ShootMultiDirection");
            yield return new WaitForSeconds(2.0f);
            EnableMovement();
            yield return new WaitForSeconds(2.0f);
        }

    }
}
