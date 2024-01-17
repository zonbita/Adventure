using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Skill
{
    void Active(string SkillName);
    void Deactive(string SkillName);
    void Update();
}
