using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill_Base : MonoBehaviour, I_Skill
{
    public List<Skill_Data_SO> skill_Data_SO;
    List<Skill_Data> _data = new List<Skill_Data>();
    TrailRenderer trailRenderer;

    private void Awake()
    {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {
        if(skill_Data_SO != null)
        {
            foreach (Skill_Data_SO s in skill_Data_SO)
            {
                _data.Add(s.GetDataInstance());
            }
        }
    }
    public void Active(string SkillName)
    {
        if (skill_Data_SO.Count > 0)
        {
            foreach (Skill_Data s in _data)
            {
                if (s.SkillName == SkillName && s.Stage == SkillStage.ready)
                {
                    switch (s.SkillName)
                    {
                        case "Teleport":
                            Vector3 direction2 = (transform.position - GameManager.Instance.Player.transform.position).normalized;
                            this.gameObject.transform.DOMove(this.gameObject.transform.position - direction2, 0.5f, false);
                            s.Stage = SkillStage.active;
                            s.currentTime = 0;
                            if (trailRenderer) StartCoroutine("TrailRender");

                            break;
                        case "ShootMultiDirection":
                                StartCoroutine("ShootMultiDirection", s);
                            break;
                        default:
                            if (s.GO_Skill)
                            {
                                Transform bullet = PoolManager.Instance.GetObject(s.GO_Skill).transform;
                                bullet.position = transform.position;
                                Vector3 direction = (GameManager.Instance.Player.transform.position - transform.position).normalized;

                                bullet.GetComponent<Bullet>()?.Init(s.Damage, true, direction * 5);

                                s.Stage = SkillStage.active;
                                s.currentTime = 0;
                            }
                            break;
                    }
                }
            }
        }
    }
    public void Active(string SkillName, Vector3 location)
    {
       
    }
    public void Deactive(string SkillName)
    {

    }

    public void Update()
    { 
        
        if(skill_Data_SO.Count > 0)
        {
            foreach (Skill_Data s in _data)
            {
                if (s.Stage == SkillStage.active)
                {
                    if (s.currentTime > s.activeTime)
                    {
                        s.currentTime = 0;
                        s.Stage = SkillStage.cooldown;
                    }
                    else s.currentTime += Time.deltaTime;
                }
                else if (s.Stage == SkillStage.cooldown)
                {
                    if (s.currentTime > s.cooldownTime)
                    { 
                        s.currentTime = 0;
                        s.Stage = SkillStage.ready;
                    }
                    else s.currentTime += Time.deltaTime;
                }
                else
                {
                    if (s.Stage != SkillStage.ready)
                        s.currentTime += Time.deltaTime;
                }

            }
        }

    }

    public void Reset(string SkillName)
    {
        foreach (Skill_Data s in _data)
        {
            if (s.SkillName == SkillName)
            {
                s.Stage =  SkillStage.ready;
                s.currentTime = 0;
            }
        }
    }

    IEnumerator ShootMultiDirection(Skill_Data s)
    {
        print("a");
        if (s.GO_Skill)
        {
            for (int index = 0; index < s.Number; index++)
            {
                // Calculate rotation
                float n = 90f * index / s.Number;

                Quaternion rotation = Quaternion.Euler(0, 0, n);
                rotation = Quaternion.AngleAxis(n/2, Vector3.forward);

                Transform bullet = PoolManager.Instance.GetObject(s.GO_Skill).transform;
                bullet.position = transform.position;
                bullet.rotation = rotation;

                Vector3 direction = (GameManager.Instance.Player.transform.position - transform.position).normalized;

                bullet.GetComponent<Bullet>()?.Init(s.Damage, true, rotation *  direction * 5);

                s.Stage = SkillStage.active;
                s.currentTime = 0;

                yield return null;
            }
        }
    }
    IEnumerator TrailRender()
    {
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(.5f);

        trailRenderer.enabled = false;

        yield return null;
    }
}
