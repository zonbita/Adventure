using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatName
{
    HP,
    Armor,
    Mana,
    Speed,
    BaseDamage,
    Regen,
    LootRadius,
    Luck
}

public enum StatType
{
    flat,
    percent
}

[System.Serializable]
public class Stat
{
    public StatName _StatName;
    public StatType _StatType;
    public float _StatValue;
}
