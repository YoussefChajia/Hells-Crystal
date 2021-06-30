using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRevive : MonoBehaviour
{
    public Player player;

    [Header("Revive UI")]
    public GameObject reviveUI;
    public Image uiCount;
    //private float waitTime = 2f;

    [Header("Revive Mechanic")]
    public bool isRevived;
    public GameObject revivePoint;

    /* public void ShowReviveUI()
    {
        reviveUI.SetActive(true);
        uiCount.fillAmount = Mathf.InverseLerp(0, 2f, waitTime);
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            waitTime = 0;
            reviveUI.SetActive(false);
            waitTime = 2f;
        }
    } */

    public void ShowReviveUI()
    {
        StartCoroutine("ShowUI");
    }

    public IEnumerator ShowUI()
    {
        reviveUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        reviveUI.SetActive(false);
    }

    public void Revive()
    {
        reviveUI.SetActive(false);
        StartCoroutine("RevivePlayer");
    }

    public IEnumerator RevivePlayer()
    {
        player.transform.position = revivePoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        isRevived = true;
    }

}
