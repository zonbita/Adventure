using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;

public class Weapon : MonoBehaviour
{
    [Header("# Game Object")]
    GameObject projectile;
    public ItemData data;

    [Header("# Value")]
    public float WeaponRate = 0.4f;
    public float WeaponRateWait = 0.1f;
    public float bulletForce = 9;
    public int prefabId;
    public int count = 1;
    public int minDamage = 6;
    public int maxDamage = 16;
    public float speed = 1;
    public int level=0;
    int DamageTotal = 1;
    float Radius = 1.5f;
    private float timer;

    private void UplevelWeapon(string s)
    {
        if (data && data.itemType.ToString() == s)
        {
            if (enabled == true && level < 10)
            {
                level++;
                StartCoroutine("Upgrade");
            }
        }
    }

    private void Start()
    {
        GameManager.Instance.UplevelWeapon += UplevelWeapon;
        Init();
    }


    private void Init()
    {
        if (!data) return;

        count = data.baseCount;
        DamageTotal = data.baseDamage;

        projectile = data.projectile;

        prefabId = GameManager.Instance.pool.GetID(data.projectile);

        switch (data.itemType)
        {
            case ItemData.ItemType.Rotator:
                InitRotator();
                break;
            case ItemData.ItemType.Homing:
                PoolManager.Instance.SpawnWait(prefabId, count);
                break;
            default:

                break;
        }
    }

    void Update()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Rotator:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case ItemData.ItemType.Range:
                timer += Time.deltaTime;
                if (timer > WeaponRate)
                {
                    timer = 0f;
                    StartCoroutine("SpawnBulletMulDirection");
                }
                break;
            case ItemData.ItemType.Homing:
                timer += Time.deltaTime;
                if (timer > WeaponRate)
                {
                    timer = 0f;
                    Transform enemy = WeaponManager.instance?.FindNearestEnemy(this.gameObject.transform.position);
                    if (enemy != null)
                    {
                        StartCoroutine("SpawnBulletHoming", enemy);
                    }
                }
                break;
            default:
                timer += Time.deltaTime;
                if (timer > WeaponRate)
                {
                    timer = 0f;
                    Transform enemy = WeaponManager.instance?.FindNearestEnemy(this.gameObject.transform.position);
                    if (enemy != null)
                    {
                        RotateGun(enemy.position);
                        StartCoroutine("SpawnBullet");
                    }
                }
                break;
        }


    }

    void RotateGun(Vector3 pos)
    {
        Vector2 lookDir = pos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = rotation;

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270) transform.localScale = new Vector3(1, -1, 0);
        else transform.localScale = new Vector3(1, 1, 0);
    }

    void InitRotator()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet;

            if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.Instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * Radius, Space.World);
            bullet.GetComponent<Bullet>()?.Init(DamageTotal + minDamage, DamageTotal + maxDamage);

        }
    }

    IEnumerator SpawnBullet()
    {
        float s = WeaponRate / count;
        for (int index = 0; index < count; index++)
        {
            Transform bullet = PoolManager.Instance.Get(prefabId).transform;
            bullet.position = transform.position;
            bullet.GetComponent<Bullet>()?.Init(DamageTotal+ minDamage, DamageTotal+ maxDamage, transform.right * bulletForce);

            yield return new WaitForSeconds(s);
        }
    }

    IEnumerator SpawnBulletHoming(Transform enemy)
    {
        float s = WeaponRate / count;
        for (int index = 0; index < count; index++)
        {
            Transform bullet = PoolManager.Instance.Get(prefabId).transform;
            if (bullet != null)
            {
                bullet.position = transform.position;
                bullet.GetComponent<Bullet_Homing>()?.Init(DamageTotal + minDamage, DamageTotal + maxDamage, enemy);
            }
            yield return new WaitForSeconds(s);
        }
    }

    IEnumerator SpawnBulletMulDirection()
    {
        for (int index = 0; index < count; index++)
        {
            Transform bullet = PoolManager.Instance.Get(prefabId).transform;
            bullet.position = transform.position;

            // Calculate rotation
            Quaternion rotation = Quaternion.Euler(0, 0, 360f * index / count);

            // Apply rotation
            bullet.rotation = rotation;

            // Translate based on the rotated up vector
            bullet.Translate(bullet.up, Space.World);

            bullet.GetComponent<Bullet>()?.Init(DamageTotal + minDamage, DamageTotal + maxDamage, rotation * Vector3.up * bulletForce);

            yield return new WaitForSeconds(0);
        }
    }

    IEnumerator Upgrade()
    {
        switch (data.itemType)
        {
            case ItemData.ItemType.Rotator:
                speed = 200 + (10 * level);
                count += data.counts[level];
                Radius = 1.5f + (0.2f * level);
                InitRotator();
                break;
            default:
                count += data.counts[level];
                //WeaponRateWait = WeaponRate / count;
                break;
        }

        float a = minDamage * data.damages[level];
        minDamage = Mathf.RoundToInt(a);
        float b = maxDamage * data.damages[level];
        maxDamage = Mathf.RoundToInt(b);

        yield return new WaitForSeconds(0);
    }

}
