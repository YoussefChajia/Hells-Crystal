using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Player player;
    public PlayerRespawn levelManager;
    public GameObject respawnPoint;
    public float stopTime = 2.5f;
    private int index;

    public GameObject[] UI;
    public Image uiCount;

    public void getOut()
    {
        uiCount.fillAmount = Mathf.InverseLerp(0, 2.5f, stopTime);

        for (int i = 0; i < UI.Length; i++)
        {
            if (i == index)
            {
                UI[i].SetActive(true);
            }
            else if (i != 2)
            {
                UI[i].SetActive(false);
            }
        }

#if UNITY_EDITOR
        if (index == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                index++;
            }
        }
        else if (index == 1)
        {
            if (Input.GetButton("Fire1"))
            {
                stopTime -= Time.deltaTime;
                if (stopTime <= 0)
                {
                    stopTime = 0;
                    StartCoroutine("Begin");
                }
            }
            if (Input.GetButtonUp("Fire1"))
            {
                stopTime = 2.5f;
            }
        }

#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (index == 0)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    index++;
                }
            }
            else if (index == 1)
            {
                if (touch.phase == TouchPhase.Stationary)
                {
                    stopTime -= Time.deltaTime;
                    if (stopTime <= 0)
                    {
                        stopTime = 0;
                        StartCoroutine("Begin");
                    }
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    stopTime = 2.5f;
                }
            }
        }
#endif

    }

    public IEnumerator Begin()
    {
        UI[2].SetActive(false);
        player.isDead = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(false);
        player.transform.position = respawnPoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        player.levels[0].gameObject.SetActive(true);
        PlayerPrefs.SetInt("Tutorial", 1);
    }
}
