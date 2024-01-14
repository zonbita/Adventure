using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    string WeaponName = "Default";
    public ItemData data;
    public Weapon Weapon;
    [SerializeField] Text LevelText;
    [SerializeField] Text DesText;
    [SerializeField] Image Img;

    private void Start()
    {
        GameManager.Instance.UpLevel += _UpLevel;
        GameManager.Instance.ReloadCards += ReloadCard;

        Init();
    }

    private void ReloadCard()
    {
        Init();
    }

    void Init()
    {
        if (data != null)
        {
            WeaponName = data.itemName;
            Img.sprite = data.itemIcon;
            LevelText.text = "Level " + (Weapon.level + 2);
            DesText.text = data.itemName+ " \n" + data.itemDesc + "\n<color=\"Green\">" + data.damages[Weapon.level] * 100 + "%</color> <color=\"red\">Damage</color>" + (data.counts[Weapon.level+1] != 0 ? "\n<color=\"orange\">+" + data.counts[Weapon.level + 1] + " Ball</color>" : "");
        }
            
    }

    private void _UpLevel()
    {
        StartCoroutine("PlayAnim");
    }


    public IEnumerator PlayAnim()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        //yield return new WaitForSeconds(0.5f);

        transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).SetUpdate(true);
        transform.DOPunchRotation(new Vector3(10, 10, 1), 1, 10, 1).SetUpdate(true);

        // Wait for the animations to finish
        yield return new WaitUntil(() => DOTween.TotalPlayingTweens() == 0);

    }

    public void MouseEnterAnim()
    {
        transform.DOScale(1.1f, 1f).SetEase(Ease.OutBounce).SetUpdate(true);
    }

    public void MouseExitAnim()
    {
        transform.DOScale(1f, 1f).SetEase(Ease.OutBounce).SetUpdate(true);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnterAnim();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseExitAnim();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance?.UplevelWeapon_(data.itemType.ToString());
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Confirm);
    }
}
