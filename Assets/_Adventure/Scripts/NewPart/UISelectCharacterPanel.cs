using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UISelectCharacterPanel : MonoBehaviour
{
    [Header("MENUS")]
    public GameObject canv_Main;
    public GameObject canv_SelectCharacter;

    public Transform Content;
    public GameObject GO_Button;
    public List<GameObject> Buttons;

    public AllPlayerData_SO AllPlayerData_SO;
    public List<PlayerData> playerDatas = new List<PlayerData>();

    public List<TextMeshProUGUI> Text;

    void Start()
    {
        ClosePanel(canv_SelectCharacter);

        foreach (PlayerData_SO data in AllPlayerData_SO.characters)
        {
            playerDatas.Add(data.GetDataInstance());
        }

        for (int i = 0; i < playerDatas.Count; i++)
        {
            PlayerData data = playerDatas[i];
            GameObject go = Instantiate(GO_Button, Content);
            go.GetComponent<Button_buy>().playerData = data;
            Buttons.Add(go);
            TextMeshProUGUI[] T = go.transform.GetChild(0).GetComponentsInChildren<TextMeshProUGUI>();
            go.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = data.SpriteCharacter;

            string info = "<color=#356070><b>" + data.playerName + "</b></color>\n";
            foreach(Stat s in data.Stats)
            {
                string a;

                switch (s._StatName)
                {
                    case StatName.BaseDamage:
                        a = "red> Base Damage:" + s._StatValue;
                        break;
                    case StatName.Speed:
                        a = "blue> Speed: " + s._StatValue;
                        break;
                    case StatName.Mana:
                        a = "pink> Mana: " + s._StatValue;
                        break;
                    case StatName.LootRadius:
                        a = "#ffa500ff> Loot Radius: " + s._StatValue;
                        break;
                    case StatName.HP: 
                        a = "#113000> Health: " + s._StatValue;
                        break;
                    case StatName.Luck:
                        a = "#800080ff> Luck x" + s._StatValue;
                        break;
                    case StatName.Regen:
                        a = "#008080ff> Regen " + s._StatValue + " HP 1/S";
                        break;
                    default:
                        a = "#140322> " + s._StatName+ ":";
                        break;
                }
                
                info = info +"\n<color=" + a + "</color>";
            }
            SetText(T, data.price.ToString(), info);
        }
    }

    public void ClosePanel(GameObject panel)
    {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
    }

    public void OpenPanel(GameObject panel)
    {
        CanvasGroup group = panel.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
    }

    public void OpenSelectCharacterCanvas()
    {
        OpenPanel(canv_SelectCharacter);
        SelectCharacter(0);
    }

    public void SelectMode(int index)
    {
        PlayerPrefs.SetInt("gameMode", index);
    }

    public void SelectCharacter(int index)
    {
        Content.transform.position = Buttons[index].transform.position;
        PlayerPrefs.SetInt("characterPreference", index);

        if (index < playerDatas.Count && playerDatas[index] != null)
        {
            //Text[0].text = playerDatas[index].maxHealth.ToString();
            Text[1].text = playerDatas[index].SpriteCharacter.ToString();
            Text[2].text = playerDatas[index].playerName;
        }
        else
        {
            Text[0].text = "Locked";
            Text[1].text = "0";
            Text[2].text = "0";
        }
    }

    void SetText(TextMeshProUGUI[] T, string s1, string s2)
    {
        T[0].text = s1;
        T[1].text = s2;
    }
}
