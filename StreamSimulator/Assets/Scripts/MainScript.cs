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
    private int scoreval;
    public Transform water;
    public GameObject a;
    public GameObject b;
    public Text finalscoretext;

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
        StartCoroutine(SubAlert());
        StartCoroutine(FoundMatch());
    }

    void Update()
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
        } else
        {
            if (!miiriovoice.isPlaying)
            {
                water.localScale = water.localScale - new Vector3(0, 0.001f, 0);
                if (water.localScale.y < 0)
                {
                    GameOver();
                }
            }
        }

        score.text = scoreval.ToString();
    }

    public void SayHi()
    {
        if (!miiriovoice.isPlaying)
        {
            miiriovoice.PlayOneShot(hiaudio[Random.Range(0, hiaudio.Length)]);
            scoreval = scoreval + 1;
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
            scoreval = scoreval + 100;
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
            scoreval = scoreval + 500;
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
            scoreval = scoreval + 10;
        } else
        {
            water.localScale = water.localScale - new Vector3(0, 0.01f, 0);
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
            yield return new WaitForSeconds(Random.Range(5, 21));
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

    public void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void GameOver()
    {
        a.SetActive(false);
        b.SetActive(true);
        StopAllCoroutines();
        Music.Stop();
        miiriovoice.Stop();
        matchfoundsound.Stop();
        SubAlertSound.Stop();
        finalscoretext.text = scoreval.ToString();
        Debug.Log("Score :" +scoreval);
        //SceneManager.LoadScene(0);
    }
}
