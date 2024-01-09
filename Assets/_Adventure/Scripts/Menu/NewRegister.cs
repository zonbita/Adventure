using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewRegister : MonoBehaviour
{
    [SerializeField] InputField InputField;
    [SerializeField] Button Btn_Play;
    [SerializeField] Button Btn_ChangeName;
    private void Start()
    {
        CheckData();
        ButtonClick(Btn_Play, OnBtnPlayClick);
        ButtonClick(Btn_ChangeName, OnBtnPlayClick);
    }

    void ButtonClick(Button button, UnityEngine.Events.UnityAction<Button> clickHandler)
    {
        button?.onClick.AddListener(() => clickHandler(button));
    }

    void OnBtnPlayClick(Button button)
    {
        if (button == Btn_Play)
        {
            if (InputField.text.ToString() != null && InputField.text.ToString() != string.Empty)
                CreateNewUserData();
        }
        if(button == Btn_ChangeName)
        {
            InputField.interactable = true;
        }

    }

    void CheckData()
    {
        string value = PlayerPrefs.GetString("PlayerData_Username");

        if (string.IsNullOrEmpty(value)) // null
        {
            InputField.interactable = true;
            Btn_Play.interactable = true;
            Btn_Play.GetComponentInChildren<Text>().text = "Set Name";
            Btn_ChangeName.gameObject.SetActive(false);
        }
        else
        {
            InputField.interactable = false;
            InputField.text = value;
            Btn_Play.interactable = true;
            Btn_Play.GetComponentInChildren<Text>().text = "Play Game";
            Btn_ChangeName.gameObject.SetActive(true);
        }
   
    }
    

    public void CreateNewUserData()
    {
        PlayerPrefs.SetString("PlayerData_Username", InputField.text);
        PlayerPrefs.SetInt("PlayerData_UserLevel", 1);
        PlayerPrefs.SetInt("PlayerData_UserGold", 0);
        PlayerPrefs.SetInt("PlayerData_UserCharacterSprite", 1);
        PlayerPrefs.SetInt("UserData", 1);
        PlayerPrefs.Save();
        Debug.Log("Data Save " + InputField.text);
    }

}
