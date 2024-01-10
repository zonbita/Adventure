using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public enum BossType { Nemesis, Assassin };


[System.Serializable]
public class Boss_Data
{
    public BossType BossType;
    public string Name;
    public Sprite SpriteCharacter;
    public AnimatorController AnimatorController;
    public int maxHealth;
    public float moveSpeed = 2f;
    public List<Skill_Data_SO> bossSkills;

}

[CreateAssetMenu(fileName = "NewBossData", menuName = "Boss Data", order = 1)]
public class Boss_Data_SO : ScriptableObject
{
    public BossType bossName;
    public string Name;
    public Sprite SpriteCharacter;
    public AnimatorController AnimatorController;
    public int maxHealth;
    public float moveSpeed;
    public List<Skill_Data_SO> bossSkills;

    public Boss_Data GetDataInstance()
    {
        return new Boss_Data()
        {
            Name = this.Name,
            SpriteCharacter = this.SpriteCharacter,
            AnimatorController = this.AnimatorController,
            maxHealth = this.maxHealth,
            moveSpeed = this.moveSpeed,
            BossType = this.bossName,
            bossSkills = this.bossSkills,
        };
    }
}