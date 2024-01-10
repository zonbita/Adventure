using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/CharacterData")]
public class PlayerData_SO : ScriptableObject
{
    public int id;
    public string playerName;
    public Sprite SpriteCharacter;
    public int currentHealth;
    public int maxHealth;
    public int price;

    public PlayerData GetDataInstance()
    {
        return new PlayerData()
        {
            id = this.id,
            playerName = this.playerName,
            SpriteCharacter = this.SpriteCharacter,
            maxHealth = this.maxHealth,
            currentHealth = this.currentHealth,
            price = this.price,
        };
    }

}

