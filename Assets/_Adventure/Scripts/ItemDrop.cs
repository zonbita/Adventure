using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemDrop : MonoBehaviour
{
    public ItemData data;

    BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
    }
    private void Start()
    {
        transform.DOPunchPosition(new Vector3(0, 1, 0), 4, 1, 0);
    }
}
