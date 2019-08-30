using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainmenuScript : MonoBehaviour
{
    public GameObject toggle;
    public GameObject back;
    public GameObject MainMenu;
    public GameObject HowtoplayMenu;
    public Slider loadingbar;
    public GameObject loadingbarobject;
    AsyncOperation async;

    public void Start()
    {
        Cursor.visible = true;
    }

    public void howtoplay()
    {
        toggle.SetActive(false);
        back.SetActive(true);
        MainMenu.SetActive(false);
        HowtoplayMenu.SetActive(true);
    }

    public void Back()
    {
        toggle.SetActive(true);
        back.SetActive(false);
        MainMenu.SetActive(true);
        HowtoplayMenu.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoRoutine());
    }

    IEnumerator StartGameCoRoutine()
    {
        toggle.SetActive(false);
        loadingbarobject.SetActive(true);

        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;

        while (async.isDone == false)
        {
            loadingbar.value = async.progress;
            if (async.progress == 0.9f)
            {
                loadingbar.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
