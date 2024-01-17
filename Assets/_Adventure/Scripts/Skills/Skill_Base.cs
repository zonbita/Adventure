using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Base : MonoBehaviour, I_Skill
{
    public List<Skill_Data_SO> skill_Data_SO;
    List<Skill_Data> _data;

    private void Start()
    {
        foreach (Skill_Data_SO s in skill_Data_SO)
        {
            _data.Add(s.GetDataInstance());
        }
    }
    public void Active(string SkillName)
    {
        foreach(Skill_Data s in _data)
        {
            if(s.SkillName == SkillName && s.Stage == SkillStage.ready)
            {
                switch (s.SkillType)
                {
                    default:
                        if (s.GO_Skill)
                        {
                            Instantiate(s.GO_Skill, this.transform.position, Quaternion.identity);
                            s.Stage = SkillStage.active;
                        }
                    break;
                }
            }
        }
    }

    public void Deactive(string SkillName)
    {
    }

    public void Update()
    {
        foreach (Skill_Data s in _data)
        {
            if (s.currenCooldownTime > s.cooldownTime)
            {
                s.currenCooldownTime = 0;
                s.Stage = SkillStage.ready;
            }
            else
            {
                s.currenCooldownTime += Time.deltaTime;
            }
        }
    }
}
