using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System.Text;
using System.Linq;

public class MGMT : MonoBehaviour
{
    GameObject theTopOne;
    GameObject theTopTwo;
    GameObject theTopTres;
    GameObject theTopCuatro;

    public List<PlayableDirector> playableDirectors;
    public List<TimelineAsset> timelines;

    public GameObject witch;
    public GameObject rice;
    public GameObject undead;
    public GameObject samurai;

    public GameObject Gwitch;
    public GameObject Grice;
    public GameObject Gundead;
    public GameObject Gsamurai;

    public GameObject TextArea;
    public GameObject TextMan;
    public Sprite oneSprite;
    int ultraCounter = 0;
    SaveFile sFl = new SaveFile();

    string[] ultraTexter = {"These are the Final Results:","Thank You All For Playing!"};

    // Use this for initialization
    void Start ()
    {
        Gwitch.SetActive(false);
        Grice.SetActive(false);
        Gundead.SetActive(false);
        Gsamurai.SetActive(false);
        Invoke("UltraTexting", 5F);
        Invoke("UltraTexting", 10F);
        Invoke("GOOH", 25F);
        sFl.ReadAll();

        int thisisAcounter = 0;
        MiniPlayerDef m1;
        MiniPlayerDef m2;
        MiniPlayerDef m3;
        MiniPlayerDef m4;
        List<MiniPlayerDef> unordered = new List<MiniPlayerDef>();

        if (sFl.magician != null)
        {
            m1 = new MiniPlayerDef(sFl.magician.fans, sFl.magician.money, sFl.magician.hp, oneSprite);
            m1.number = 1;
            thisisAcounter++;
            unordered.Add(m1);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        if (sFl.undead != null)
        {
            m2 = new MiniPlayerDef(sFl.undead.fans, sFl.undead.money, sFl.undead.hp, oneSprite);
            m2.number = 3;
            thisisAcounter++;
            unordered.Add(m2);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        if (sFl.samurai != null)
        {
            m3 = new MiniPlayerDef(sFl.samurai.fans, sFl.samurai.money, sFl.samurai.hp, oneSprite);
            m3.number = 2;
            thisisAcounter++;
            unordered.Add(m3);
        }
        else
        {
            unordered.Add(new MiniPlayerDef());
        }
        if (sFl.rice != null)
        {
            m4 = new MiniPlayerDef(sFl.rice.fans, sFl.rice.money, sFl.rice.hp, oneSprite);
            m4.number = 4;
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
            if (iccc == 0)
            {
                GoldStatue(ordered[iccc].number);
                SpawnTop(ordered[iccc].number);
                AnimateSmth(1,theTopOne);
            }
            else if (iccc == 1)
            {
                SpawnDos(ordered[iccc].number);
                AnimateSmth(2,theTopTwo);
            }
            else if (iccc == 2)
            {
                SpawnTres(ordered[iccc].number);
                AnimateSmth(3,theTopTres);
            }
            else
            {
                SpawnCuatro(ordered[iccc].number);
                AnimateSmth(4,theTopCuatro);
            }
        }
    }

    //To quit
    public void GOOH()
    {
        Application.Quit();
    }

    public void GoldStatue(int nummmer)
    {
        switch (nummmer)
        {
            case 1:
                Gwitch.SetActive(true);
                break;
            case 2:
                Gsamurai.SetActive(true);
                break;
            case 3:
                Gundead.SetActive(true);
                break;
            case 4:
                Grice.SetActive(true);
                break;
            default:
                break;
        }
    }

    //To show some text
    public void UltraTexting()
    {
        TextMan.gameObject.GetComponent<Text>().text = ultraTexter[ultraCounter];
        ultraCounter = 1;
    }

    // 1:Witch, 2:RiceMonk, 3:Samurai, 4:Undead
    private void SpawnTop(int charM8)
    {
        switch (charM8)
        {
            case 1:
                theTopOne = Instantiate(witch, new Vector3(0.331f, 1.478f, -0.44f), Quaternion.Euler(0.0f, 153.895f, 0.0f));
                //theTopOne.transform.localScale = new Vector3(78F, 78F, 78F);
                break;
            case 2:
                theTopOne = Instantiate(samurai, new Vector3(0.331f, 1.478f, -0.44f), Quaternion.Euler(0.0f, 153.895f, 0.0f));
                break;
            case 3:
                theTopOne = Instantiate(undead, new Vector3(0.331f, 1.478f, -0.44f), Quaternion.Euler(0.0f, 153.895f, 0.0f));
                break;
            case 4:
                theTopOne = Instantiate(rice, new Vector3(0.331f, 1.478f, -0.44f), Quaternion.Euler(0.0f, 153.895f, 0.0f));
                break;
            default:
                break;
        }
    }

    // 1:Witch, 2:RiceMonk, 3:Samurai, 4:Undead
    private void SpawnDos(int charM8)
    {
        switch (charM8)
        {
            case 1:
                theTopTwo = Instantiate(witch, new Vector3(-2.18f, 0.049f, -1.32f), Quaternion.Euler(0.0f, 113.038f, 0.0f));
                break;
            case 2:
                theTopTwo = Instantiate(samurai, new Vector3(-2.18f, 0.049f, -1.32f), Quaternion.Euler(0.0f, 113.038f, 0.0f));
                break;
            case 3:
                theTopTwo = Instantiate(undead, new Vector3(-2.18f, 0.049f, -1.32f), Quaternion.Euler(0.0f, 113.038f, 0.0f));
                break;
            case 4:
                theTopTwo = Instantiate(rice, new Vector3(-2.18f, 0.049f, -1.32f), Quaternion.Euler(0.0f, 113.038f, 0.0f));
                break;
            default:
                break;
        }
    }
    // 1:Witch, 2:RiceMonk, 3:Samurai, 4:Undead
    private void SpawnTres(int charM8)
    {
        switch (charM8)
        {
            case 1:
                theTopTres = Instantiate(witch, new Vector3(3.224f, 0.01f, -0.177f), Quaternion.Euler(0.0f, 220.55f, 0.0f));
                break;
            case 2:
                theTopTres = Instantiate(samurai, new Vector3(3.224f, 0.01f, -0.177f), Quaternion.Euler(0.0f, 220.55f, 0.0f));
                break;
            case 3:
                theTopTres = Instantiate(undead, new Vector3(3.224f, 0.01f, -0.177f), Quaternion.Euler(0.0f, 220.55f, 0.0f));
                break;
            case 4:
                theTopTres = Instantiate(rice, new Vector3(3.224f, 0.01f, -0.177f), Quaternion.Euler(0.0f, 220.55f, 0.0f));
                break;
            default:
                break;
        }
    }
    // 1:Witch, 2:RiceMonk, 3:Samurai, 4:Undead
    private void SpawnCuatro(int charM8)
    {
        switch (charM8)
        {
            case 1:
                theTopCuatro = Instantiate(witch, new Vector3(8.595f, -1.852f, 2.852f), Quaternion.Euler(0.0f, -160.216f, 0.0f));
                break;
            case 2:
                theTopCuatro = Instantiate(samurai, new Vector3(8.595f, -1.852f, 2.852f), Quaternion.Euler(0.0f, -160.216f, 0.0f));
                break;
            case 3:
                theTopCuatro = Instantiate(undead, new Vector3(8.595f, -1.852f, 2.852f), Quaternion.Euler(0.0f, -160.216f, 0.0f));
                break;
            case 4:
                theTopCuatro = Instantiate(rice, new Vector3(8.595f, -1.852f, 2.852f), Quaternion.Euler(0.0f, -160.216f, 0.0f));
                break;
            default:
                break;
        }
    }

    public void AnimateSmth(int thisnum,GameObject theOneMagic)
    {
        PlayableDirector director = playableDirectors[0];
        IEnumerable<TrackAsset> tr = timelines[0].GetOutputTracks();
        int cc = 0;
        foreach (var item in tr)
        {
            if (cc == thisnum)
            {
                director.SetGenericBinding(item, theOneMagic);
            }
            cc++;
        }
        director.Play();
    } 

    // Update is called once per frame
    void Update () {
		
	}
}
