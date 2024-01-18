using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character/CharacterData")]
public class PlayerData_SO : ScriptableObject
{
    public int id;
    public string playerName;
    public Sprite SpriteCharacter;
    public int price;
    public List<Stat> Stats;
    public PlayerData GetDataInstance()
    {
        return new PlayerData()
        {
            id = this.id,
            playerName = this.playerName,
            SpriteCharacter = this.SpriteCharacter,
            price = this.price,
            Stats = this.Stats
        };
    }

}

