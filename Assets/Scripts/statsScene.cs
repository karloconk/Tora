using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

public class statsScene : MonoBehaviour
{
    public Image[] circlesmen;
    public Sprite[] pIcons;
    public List<GameObject> Players;
    public List<GameObject> fanTexts;
    public List<GameObject> hpTexts;
    public List<GameObject> moneyTexts;
    List<MiniPlayerDef> playersLittle;
    SaveFile sFl = new SaveFile();
    GameManager gameManagerDelJuego;
    public GameObject savingG;
    public GameObject upperText;
    public GameObject SaveArea;
    public GameObject OKArea;
    public GameObject SaveFilesArea;
    public GameObject S1;
    public GameObject S2;

    // Use this for initialization
    void Start ()
    {
        StatusMaker sm = new StatusMaker();
        sm.SetCore("");
        SaveFilesArea.SetActive(false);
        gameManagerDelJuego = GameManager.Instance;
        savingG.gameObject.GetComponent<Text>().text = " ";
        if (gameManagerDelJuego.reanudado == true)
        {
            upperText.gameObject.GetComponent<Text>().text = "This is how things were the last time you were here, friends.";
            SaveArea.SetActive(false);
            OKArea.SetActive(false);
            Invoke("ReadyToGo", 5f);
            string savefilefolder = gameManagerDelJuego.savefile == 0 ? savefilefolder = "save0" : savefilefolder = "save1";
            sFl.ReadAllSave(savefilefolder);
            AssignAllToGameManager();
        }
        else
        {
            OKArea.SetActive(false);
            sFl.ReadAll();
        }
        int thisisAcounter = 0;
        MiniPlayerDef m1;
        MiniPlayerDef m2;
        MiniPlayerDef m3;
        MiniPlayerDef m4;
        List<MiniPlayerDef> unordered = new List<MiniPlayerDef>();

        if(sFl.magician != null)
        {
            m1 = new MiniPlayerDef(sFl.magician.fans,sFl.magician.money,sFl.magician.hp, pIcons[0]);
            thisisAcounter++;
            unordered.Add(m1);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        if(sFl.undead != null)
        {
            m2 = new MiniPlayerDef(sFl.undead.fans,sFl.undead.money,sFl.undead.hp, pIcons[1]);
            thisisAcounter++;
            unordered.Add(m2);
        }    
        else
        {
            unordered.Add(new MiniPlayerDef());
        }    
        if(sFl.samurai != null)
        {
            m3 = new MiniPlayerDef(sFl.samurai.fans,sFl.samurai.money,sFl.samurai.hp, pIcons[2]);
            thisisAcounter++;
            unordered.Add(m3);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        if(sFl.rice != null)
        {
            m4 = new MiniPlayerDef(sFl.rice.fans,sFl.rice.money,sFl.rice.hp, pIcons[3]);
            thisisAcounter++;
            unordered.Add(m4);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        List<MiniPlayerDef> ordered = unordered.OrderBy(o => o.health).ToList();
        ordered = ordered.OrderBy(o => o.wealth).ToList();
        ordered = ordered.OrderBy(o => o.fans).ToList();
        ordered.Reverse();
        for (int iccc = 0; iccc < ordered.Count; iccc++)
        {
            if (ordered[iccc].fans != 0 || ordered[iccc].wealth != 0 || ordered[iccc].health != 0)
            {
                fanTexts[iccc].gameObject.GetComponent<Text>().text = "Fans: " + ordered[iccc].fans;
                hpTexts[iccc].gameObject.GetComponent<Text>().text = "Health: " + ordered[iccc].health;
                moneyTexts[iccc].gameObject.GetComponent<Text>().text = "Wealth: " + ordered[iccc].wealth;
                circlesmen[iccc].sprite = ordered[iccc].iconnn;
            }
            else
            {
                Players[iccc].SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void ReadyToGo()
    {
        OKArea.SetActive(true);
    }

    public void SaveButton()
    {
        SaveFilesArea.SetActive(true);
    }

    public void savefiles0()
    {
        S1.SetActive(false);
        S2.SetActive(false);
        Debug.Log(gameManagerDelJuego);
        Debug.Log(gameManagerDelJuego.savefile);
        gameManagerDelJuego.savefile = 0;
        savingG.gameObject.GetComponent<Text>().text = "Saving Game";
        upperText.gameObject.GetComponent<Text>().text = "Please Wait...";
        //GAMEMANAGER NUMJUEGO
        sFl.SaveGame(gameManagerDelJuego.savefile);
        Invoke("returnToGame", 4f);
    }

    public void savefiles1()
    {
        S1.SetActive(false);
        S2.SetActive(false);
        gameManagerDelJuego.savefile = 1;
        savingG.gameObject.GetComponent<Text>().text = "Saving Game";
        upperText.gameObject.GetComponent<Text>().text = "Please Wait...";
        //GAMEMANAGER NUMJUEGO
        sFl.SaveGame(gameManagerDelJuego.savefile);
        Invoke("returnToGame", 4f);
    }

    public void NotSaveButton()
    {
        Invoke("returnToGame", 0f);
    }

    public void returnToGame()
    {
        Debug.Log("ya me fui");
        gameManagerDelJuego.NombreNivelQueSeVaCargar = "QueSeCargueElJuego:v";
        SceneManager.LoadScene("PantallaCargandoLoadingScreen");
    }

    public void AssignAllToGameManager()
    {
        gameManagerDelJuego.undead = sFl.undead;
        gameManagerDelJuego.witch = sFl.magician;
        gameManagerDelJuego.samurai = sFl.samurai;
        gameManagerDelJuego.ricemonk = sFl.rice;

        gameManagerDelJuego.firstLoadMainScene = false;
        gameManagerDelJuego.firstTimeTutorial = new bool[4]
        {
            true,
            true,
            true,
            true
        };

        gameManagerDelJuego.numberOfPlayers = sFl.coreD.numPlayers;
        gameManagerDelJuego.orderedPlayers = sFl.coreD.orderPlayers;
        gameManagerDelJuego.orderedPlayersID = sFl.coreD.orderPlayersID;
        gameManagerDelJuego.turnsCount = sFl.coreD.turnsCounted;
        gameManagerDelJuego.moonPhase = sFl.coreD.monnphase;
        gameManagerDelJuego.deadMoonPhase = sFl.coreD.deadmoon;
        gameManagerDelJuego.samePlayer = sFl.coreD.samePlayer;
        gameManagerDelJuego.playerDictionary = sFl.coreD.playerDictionary;
        // gameManagerDelJuego.currentSaviour = sFl.coreD.currentSaviour;
        gameManagerDelJuego.currentPlayer = sFl.coreD.currentPlayer;
        Debug.Log("playerDict: " + gameManagerDelJuego.playerDictionary);
        
		Debug.Log("currentPLay: "+ gameManagerDelJuego.currentPlayer);
        gameManagerDelJuego.bossWinners = sFl.coreD.bossWinners;
        gameManagerDelJuego.areaCleared = sFl.coreD.areaCleared;
    }
}

public class MiniPlayerDef
{
    public int position;
    public int fans;
    public int wealth;
    public int health;
    public int number;
    public Sprite iconnn;

    public MiniPlayerDef(int fan,int wealt,int healt, Sprite iconnmn)
    {
        fans   = fan;
        wealth = wealt;
        health = healt>=0 ? healt : 0;
        iconnn = iconnmn;
    }

    public MiniPlayerDef()
    {
        fans   = -10000;
        wealth = -100000;
        health = -100000;
    }
}
