using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Skill
{
    void Active(string SkillName);
    void Active(string SkillName, Vector3 location);

    void Deactive(string SkillName);

    void Reset(string SkillName);
}
