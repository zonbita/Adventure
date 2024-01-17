using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Teleport, Projectile, RocketHomming };
public enum SkillStage {ready, active, cooldown };

[System.Serializable]
public class Skill_Data
{
    public SkillType SkillType;
    public SkillStage Stage;
    public string SkillName;
    public GameObject GO_Skill;
    public float cooldownTime = 1;
    public float activeTime = 1;
    public int Damage = 6;
    public float currenCooldownTime = 0;
}

[CreateAssetMenu(fileName = "New Skill Data", menuName = "Skill Data", order = 1)]
public class Skill_Data_SO : ScriptableObject
{
    public SkillType SkillType;
    public SkillStage Stage;
    public string SkillName;
    public GameObject GO_Skill;
    public float cooldownTime = 1;
    public float activeTime = 1;
    public int Damage = 6;

    public Skill_Data GetDataInstance()
    {
        return new Skill_Data()
        {
            SkillType = this.SkillType,
            Stage = this.Stage,
            SkillName = this.SkillName,
            GO_Skill = this.GO_Skill,
            cooldownTime = this.cooldownTime,
            activeTime = this.activeTime,
            Damage = this.Damage,
        };
    }

}