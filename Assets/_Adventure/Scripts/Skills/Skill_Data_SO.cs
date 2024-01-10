using System.Collections.Generic;
using UnityEngine;

public enum BossSkill { Teleport, Projectile, RocketHomming };


[System.Serializable]
public class Skill_Data
{
    public BossSkill bossSkill;
    public GameObject GO_Skill;
    public int minDamage = 6;
    public int maxDamage = 16;
}

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill Data", order = 1)]
public class Skill_Data_SO : ScriptableObject
{
    public BossSkill bossSkill;
    public GameObject GO_Skill;
    public int minDamage = 6;
    public int maxDamage = 16;

    public Skill_Data GetDataInstance()
    {
        return new Skill_Data()
        {
            bossSkill = this.bossSkill,
            GO_Skill = this.GO_Skill,
            minDamage = this.minDamage,
            maxDamage = this.maxDamage
        };
    }
}