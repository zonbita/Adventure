using DG.Tweening;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum Phase{
    Home,
    Start,
    Win,
    close,
    Pause,
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public System.Action GameOver, GameWin, GameStart, GamePause, GameResume, GameRevive, GameInit, UpLevel, ReloadCards;
    public System.Action<GameObject> AddMonster;
    public System.Action<string> UplevelWeapon;
    public List<GameObject> Monster;

    void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
    }

    
    public List<TextMeshProUGUI> totalCoinTMP;
    [Header("# Game Object")]
    public PoolManager pool;
    public GameObject CoinPrefab;
    public GameObject damPopUp;
    public GameObject DeathEffectPrefab;
    public Player Player;

    [Header("# Panel List")]
    public GameObject SettingPanel;
    public GameObject LosePanel;
    public GameObject ListCard;

    // Level EXP
    public Image Levelbar;
    public TMP_Text LevelText;
    public TextMeshProUGUI score;
    int currentExp = 0;
    int currentLevel = 1;
    int requireExp = 10;
    public GameObject LevelPanel;

    // KILL
    public TextMeshProUGUI KillText;
    [HideInInspector] public int currentKilled = 0;

    // Coin
    private int totalCoin = 0;

    // Prefab ID
    [HideInInspector] public int CoinID = -1;
    [HideInInspector] public int DeathEffectID = -1;
    [HideInInspector] public int PopupID = -1;

    // START
    public void Start()
    {
        StartGame();
    }

    public void StartGame()
    {   
        // GET ID
        CoinID = pool.GetID(CoinPrefab);
        DeathEffectID = pool.GetID(DeathEffectPrefab);
        PopupID = pool.GetID(damPopUp);
        KillText.text = "0";

        AudioManager.instance.PlayBgm(true);

        GameOver += () =>
        {
            score.text = "You get: " + (currentKilled * 10).ToString() + " Score";
            OpenWindow(1);
        };

        GameWin += () =>
        {

        };

        GameStart += () =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
        GameRevive += () =>
        {
            
        };
    }
    public int Kill
    {
        get => currentKilled;
        set
        {
            currentKilled++;
            KillText.text = currentKilled.ToString();
        }
    }
    public int TotalCoin
    {
        get => totalCoin;
        set { totalCoin = value; foreach (var tmp in totalCoinTMP) tmp.SetText(totalCoin + ""); }
    }

    public void UplevelWeapon_(string s)
    {
        CloseLevelUpPanel();
        UplevelWeapon(s);
        ReloadCards();
    }


    public void Restart()
    {
        GameStart();
    }
    public int Level
    {
        get => currentLevel;
        set
        {
            if (currentLevel == value) return;

            currentLevel++;
        }
    }

    public void Revive()
    {
        if(TotalCoin >= 2)
        {
            TotalCoin -= 2;
            CloseWindow(1);
        }
        else
        {
            LosePanel.GetComponentInChildren<Button>();
        }
    }

    public void SpawnCoin(Vector3 p)
    {
        Transform coin = pool.Get(CoinID).transform;
        coin.position = p;
    }

    public void SpawnDeathEffect(Vector3 p)
    {
        Transform d = pool.Get(DeathEffectID).transform;
        d.position = p;
        SpawnCoin(p);
        Kill++;
    }



    public void UpdateExperience(int addExp)
    {
        currentExp += addExp;
        if (currentExp >= requireExp)
        {
            currentLevel++;
            currentExp = currentExp - requireExp;
            requireExp = (int)(requireExp * 1.5f);
            OpenLevelUpPanel();

            switch (currentLevel)
            {
                case 4:
                    AddMonster(Monster[0]);
                    break;

                case 6:
                    AddMonster(Monster[1]);
                    break;

                case 8:
                    AddMonster(Monster[2]);
                    break;

                default:
                    break;
            }
            UpLevel();
        }


        UpdateBar(currentExp, requireExp, "Level " + currentLevel.ToString());
    }

    public void OpenWindow(int i)
    {
        CanvasGroup group;
        switch (i)
        {
            case 0:
                group = SettingPanel.GetComponent<CanvasGroup>();
                group.alpha = 1;
                group.blocksRaycasts = true;
                group.interactable = true;
                Time.timeScale = 0;
                break;
            case 1:
                group = LosePanel.GetComponent<CanvasGroup>();
                group.alpha = 1;
                group.blocksRaycasts = true;
                group.interactable = true;
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }
    public void CloseWindow(int i)
    {
        CanvasGroup group;
        switch (i)
        {
            case 0:
                group = SettingPanel.GetComponent<CanvasGroup>();
                group.alpha = 0;
                group.blocksRaycasts = false;
                group.interactable = false;
                Time.timeScale = 1;
                break;
            case 1:
                group = LosePanel.GetComponent<CanvasGroup>();
                group.alpha = 0;
                group.blocksRaycasts = false;
                group.interactable = false;
                GameRevive();
                break;
            default:
                break;
        }
    }

    public void UpdateBar(int value, int maxValue, string text)
    {
        if(Levelbar) Levelbar.fillAmount = (float)value / (float)maxValue;
        LevelText.SetText(text);
    }

    public void CloseLevelUpPanel()
    {
        CanvasGroup group = LevelPanel.GetComponent<CanvasGroup>();
        group.alpha = 0;
        group.blocksRaycasts = false;
        group.interactable = false;
        Time.timeScale = 1;
    }

    public void OpenLevelUpPanel()
    {
        CanvasGroup group = LevelPanel.GetComponent<CanvasGroup>();
        group.alpha = 1;
        group.blocksRaycasts = true;
        group.interactable = true;
        Time.timeScale = 0;
    }
}
