using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [Header("Respawn Mechanic")]
    [SerializeField] private GameObject respawnPoint;

    [Header("UI Tools")]
    [SerializeField] private float stopTime;
    [SerializeField] private GameObject[] UI;
    [SerializeField] private Image uiCount;
    private int index;

    public GameObject[] getUI()
    {
        return this.UI;
    }

    public void Begin()
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
                    StartCoroutine("Finish");
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
                        StartCoroutine("Finish");
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

    public IEnumerator Finish()
    {
        LevelManager levelManager = Player.instance.getLevelManager();
        UI[2].SetActive(false);
        Player.instance.setIsDead(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(false);
        Player.instance.transform.position = respawnPoint.transform.position;
        Player.instance.setIsDead(false);
        Player.instance.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(true);
        Player.instance.getLevelManager().getLevels()[0].gameObject.SetActive(true);
        Player.instance.getLevelManager().setActiveLevel(Player.instance.getLevelManager().getLevels()[0]);
        Player.instance.getLevelManager().getActiveLevel().InitializeLevel();
        PlayerPrefs.SetInt("Tutorial", 1);
    }
}
