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
            SetText(T, data.price.ToString(), data.playerName);
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
            Text[0].text = playerDatas[index].maxHealth.ToString();
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
