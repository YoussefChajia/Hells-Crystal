using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRevive : MonoBehaviour
{
    public Player player;
    public PlayerRespawn levelManager;

    [Header("Revive UI")]
    public GameObject reviveUI;
    public Image uiCount;
    public Button revive;
    public float waitTime = 2f;

    [Header("Revive Mechanic")]
    [HideInInspector]
    public bool isRevived;
    public GameObject revivePoint;

    private void Update()
    {
        if (levelManager.isRevivable)
        {
            ShowReviveUI();
        }
    }

    public void ShowReviveUI()
    {
        reviveUI.SetActive(true);
        uiCount.fillAmount = Mathf.InverseLerp(0, 2f, waitTime);
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            waitTime = 0;
            levelManager.isRevivable = false;
            levelManager.Respawn();
        }
    }

    public void Revive()
    {
        levelManager.isRevivable = false;
        reviveUI.SetActive(false);
        waitTime = 2f;
        StartCoroutine("RevivePlayer");
    }

    public IEnumerator RevivePlayer()
    {
        //yield return new WaitForSeconds(2f);
        player.transform.position = revivePoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        isRevived = true;
    }

}
