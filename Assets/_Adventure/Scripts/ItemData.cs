using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Weapon/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Default, Melee, Range, Rotator, Tool }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    [TextArea]
    public string itemDesc;
    public Sprite itemIcon;

    [Header("# Level Data")]
    public int baseDamage;
    public int baseCount;
    public float[] damages;
    public int[] counts;

    [Header("# Weapon")]
    public GameObject projectile;

}