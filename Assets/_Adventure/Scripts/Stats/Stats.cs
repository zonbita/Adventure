using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class Stats : ScriptableObject
{
    public StatName _StatName = StatName.HP;
    public StatType _StatType = StatType.percent;
    public float _StatValue;

    public Stat GetInstance()
    {
        return new Stat()
        {
            _StatValue = this._StatValue,
            _StatType = this._StatType,
            _StatName = this._StatName
        };
    }

    public void Moditify(float newValue)
    {
        _StatValue += newValue;
    }
}

