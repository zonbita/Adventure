using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_buy : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]public PlayerData playerData;

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
        AudioManager.instance?.PlaySfx(AudioManager.Sfx.Confirm);
        if(GameManager.Instance.TotalCoin > playerData.price)
        {
            GameManager.Instance.TotalCoin -= playerData.price;
            GameManager.Instance.Player.ChangeSprite(playerData.SpriteCharacter);
            GameManager.Instance.CloseWindow(2);
        }
    }
}
