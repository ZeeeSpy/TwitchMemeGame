using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    private AudioClip[] hiaudio;
    private AudioClip[] plugaudio;
    private AudioClip[] skipaudio;
    private AudioClip[] subaudio;
    private AudioClip[] tyaudio;
    private Sprite[] characters;

    private AudioClip[] GoodMusic;
    private AudioClip[] ShitMusic;
    public AudioClip money;
    private bool shitmusicplaying = false;
    private bool subactive = false;
    private bool satisfied = false;
    private bool matchfoundmode = false;
    private bool matchplugged = false;

    public AudioSource miiriovoice;

    public SpriteRenderer matchfound;
    public GameObject matchfoundobject;
    public AudioSource matchfoundsound;
    public AudioSource SubAlertSound;

    public VideoPlayer theactualplayer;
    public AudioSource Music;
    public Text score;
    private double scoreval = 0;
    public Transform water;
    public GameObject a;
    public GameObject b;
    public Text finalscoretext;


    private int currenttime = 30;
    public Text countdowntime;

    private bool gameoverstatus = false;
    private bool shoptime = false;


    public GameObject ShopStuff;
    public GameObject ShopImage;
    public GameObject StuffToDeactive;

    public GameObject Father;
    public GameObject Combo;
    public GameObject Jerry;
    public GameObject Jersey;
    public GameObject Xbox;

    private double multiplier = 1;
    
    public Text koreanleveltext;
    public Text shopmoney;
    public Text multipliertext;

    private double modval = 1.00;
    private int koreanlvl = 0;
    private int hi = 0;

    private bool ZeSpy = false;
    public GameObject zespytext;
    private bool win = false;
    public GameObject wintext;

    void Awake()
    {
        theactualplayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Background Vid.mp4");
        theactualplayer.Play();
        hiaudio = Resources.LoadAll<AudioClip>("Sounds/hi");
        plugaudio = Resources.LoadAll<AudioClip>("Sounds/plug");
        skipaudio = Resources.LoadAll<AudioClip>("Sounds/skip");
        subaudio = Resources.LoadAll<AudioClip>("Sounds/sub");
        tyaudio = Resources.LoadAll<AudioClip>("Sounds/ty");
        

        GoodMusic = Resources.LoadAll<AudioClip>("Music/GoodMusic");
        ShitMusic = Resources.LoadAll<AudioClip>("Music/ShitMusic");

        characters = Resources.LoadAll<Sprite>("Characters");

        Music.PlayOneShot(GoodMusic[Random.Range(0, GoodMusic.Length)]);
        StartCoroutine("SubAlert");
        StartCoroutine("FoundMatch");
        StartCoroutine("Countdown");
    }

    void Update()
    {
        if (!shoptime)
        {

            if (!Music.isPlaying)
            {
                if ((int)Random.Range(0, 3) == 0)
                {
                    shitmusicplaying = false;
                    Music.PlayOneShot(GoodMusic[Random.Range(0, GoodMusic.Length)]);
                }
                else
                {
                    shitmusicplaying = true;
                    Music.PlayOneShot(ShitMusic[Random.Range(0, ShitMusic.Length)]);
                }
            }

            if (shitmusicplaying)
            {
                water.localScale = water.localScale - new Vector3(0, 0.004f, 0);
                if (water.localScale.y < 0)
                {
                    GameOver();
                }
            }
            else
            {
                    water.localScale = water.localScale - new Vector3(0, 0.001f, 0);
                    if (water.localScale.y < 0)
                    {
                        GameOver();
                    }
            }

            score.text = scoreval.ToString();
        } else
        {
            //shopt time 
        }
    }

    public void SayHi()
    {
        if (!miiriovoice.isPlaying)
        {
            miiriovoice.PlayOneShot(hiaudio[hi]);
            hi++;
            if (hi >= hiaudio.Length)
            {
                hi = 0;
            }
            scoreval = scoreval + (0.50*multiplier);
        }
    }

    public void AltF4()
    {
        if (matchfoundmode)
        {
            matchplugged = true;
            matchfound.enabled = false;
            matchfoundmode = false;
            matchfoundobject.SetActive(false);
            matchfoundsound.Stop();
            miiriovoice.Stop();
            miiriovoice.PlayOneShot(plugaudio[Random.Range(0, plugaudio.Length)]);
            scoreval = scoreval + (1.00*multiplier);
        } 
    }

    public void Sub()
    {
        if (subactive)
        {
            miiriovoice.Stop();
            SubAlertSound.Stop();
            miiriovoice.PlayOneShot(tyaudio[Random.Range(0, tyaudio.Length)]);
            satisfied = true;
            scoreval = scoreval + (2.50*multiplier);
        }  else if (!miiriovoice.isPlaying && !subactive)
        {
            water.localScale = water.localScale - new Vector3(0, 0.02f, 0);
        }
    }

    public void SkipSong()
    {
        if (shitmusicplaying)
        {
            Music.Stop();
            shitmusicplaying = false;
            scoreval = scoreval + (1.00*multiplier);
        } else
        {
            water.localScale = water.localScale - new Vector3(0, 0.01f, 0);
        }
    }


    IEnumerator Countdown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currenttime--;
            if (currenttime == 0 && !gameoverstatus)
            {
                OpenShop();
            }
            else
            {
                countdowntime.text = currenttime.ToString();
            }
        }
    }


    IEnumerator SubAlert()
    {
        while (true){
            yield return new WaitForSeconds(Random.Range(20, 50));
            SubAlertSound.PlayOneShot(subaudio[Random.Range(0, subaudio.Length)]);
            subactive = true;
            yield return new WaitForSeconds(15);
            if (!satisfied)
            {
                water.localScale = water.localScale - new Vector3(0, 0.1f, 0);
            }
            else
            {
                subactive = false;
                satisfied = false;
            }
        }
    }

    IEnumerator FoundMatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 15));
            matchfound.sprite = characters[Random.Range(0, characters.Length)];
            matchfoundobject.SetActive(true);
            matchfound.enabled = true;
            matchfoundsound.Play();
            matchfoundmode = true;
            yield return new WaitForSeconds(5);
            if (!matchplugged)
            {
                water.localScale = water.localScale - new Vector3(0, 0.1f, 0);
            } else
            {
                matchplugged = false;
            }
        }
    }

    private void OpenShop()
        {
            shoptime = true;
            Debug.Log("Shopping Time");
            StopCoroutine("SubAlert");
            StopCoroutine("FoundMatch");
            Music.Stop();
            miiriovoice.Stop();
            SubAlertSound.Stop();
            matchfoundsound.Stop();
            ShopStuff.SetActive(true);
            ShopImage.SetActive(true);
            StuffToDeactive.SetActive(false);
            shopmoney.text = scoreval.ToString();   

            if (!win)
            {
            wintext.SetActive(false);
            }

            if (!ZeSpy)
            {
            zespytext.SetActive(false);
            }
        }

    public void exitshop()
    {
        shoptime = false;
        Debug.Log("Shopping Time Ended");
        StartCoroutine("SubAlert");
        StartCoroutine("FoundMatch");
        ShopStuff.SetActive(false);
        ShopImage.SetActive(false);
        StuffToDeactive.SetActive(true);

        currenttime = 30;
        score.text = scoreval.ToString();
    }

    public void BuyMod(int a)
    {
        if (Buycalculate(a))
        {
            modval = a;
            calculatemultiplier();
            UpdateShop();
        }
    }

    public void BuyKoreanLesson()
    {
        if (Buycalculate(25))
        {
            koreanlvl = koreanlvl + 1;
            calculatemultiplier();
            UpdateShop();
        }
    }

    public void RefillBottle()
    {
        if (Buycalculate(2)) {
            water.localScale = new Vector3(2.5f, 5, 1);
        }
    }

    public void BuyFather()
    {
        if (Buycalculate(2)) { Father.SetActive(true); }
    }


    public void BuyCombo()
    {
        if (Buycalculate(2)) { Combo.SetActive(true); }
    }


    public void BuyJerry()
    {
        if (Buycalculate(5)) { Jerry.SetActive(true); }
    }


    public void BuyJersey()
    {
        if (Buycalculate(100)) { Jersey.SetActive(true); }
    }


    public void BuyXbox()
    {
        if (Buycalculate(1000)) { Xbox.SetActive(true); }
    }

    public void BuyZeSpy()
    {
        if (Buycalculate(4000000)) { zespytext.SetActive(true); }
        ZeSpy = true;
    }

    public void BuyBandi()
    {
        if (Buycalculate(1000000000)) { wintext.SetActive(true); }
        win = true;
    }

    private bool Buycalculate(int price)
    {
        if (scoreval >= price)
        {
            scoreval = scoreval - price;
            miiriovoice.PlayOneShot(money);
            UpdateShop();
            return true;
        } else
        {
            return false;
        }
    }


    private void UpdateShop()
        {
            calculatemultiplier();
            shopmoney.text = scoreval.ToString();
            multipliertext.text = multiplier.ToString();
            koreanleveltext.text = koreanlvl.ToString();
        }

    private void calculatemultiplier()
    {
        multiplier = koreanlvl*modval;
        if (multiplier <= 0)
        {
            multiplier = 1;
        }
    }

    public void PlayAgain()
        {
            SceneManager.LoadScene(1);
        }

        public void GameOver()
        {
            gameoverstatus = true;
            a.SetActive(false);
            b.SetActive(true);
            StopAllCoroutines();
            Music.Stop();
            miiriovoice.Stop();
            matchfoundsound.Stop();
            SubAlertSound.Stop();
            finalscoretext.text = scoreval.ToString();
            Debug.Log("Score :" +scoreval);
            Cursor.visible = true;
        }

}
