using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwitchChatScript : MonoBehaviour
{
    public GameObject twtichchat;

    private void Start()
    {
        StartCoroutine(TwitchChatMove());

    }

    //-4.12
    //6.2

    IEnumerator TwitchChatMove()
    {
        while (true)
        {
            twtichchat.transform.position = twtichchat.transform.position - new Vector3(0, Random.Range(0.5f, 1.5f), 0);
            if (twtichchat.transform.position.y < (-4.12))
            {
                twtichchat.transform.position = new Vector3(7.2f, 6.2f, 0);
            }
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }
}