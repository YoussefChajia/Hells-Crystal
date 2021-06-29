using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRevive : MonoBehaviour
{
    [Header("Revive UI")]
    public Player player;
    public GameObject reviveUI;
    public Image uiCount;
    private float waitTime = 2f;
    public GameObject revivePoint;

    [Header("Revive Mechanic")]
    public PlayerRespawn levelManager;
    public bool isRevived;
    public bool isReviving;

    void Update()
    {
        if (levelManager.isrevivable && !isRevived)
        {
            AskPlayer();
        }
    }

    public void AskPlayer()
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
    }

    public void GetRevive()
    {
        //if ad has been watched
        //Revive method logic needs to be triggered from rewarded video script
        //This is just a comment test for git and changes
    }

    public void Revive()
    {
        isReviving = true;
        StartCoroutine("RevivePlayer");
    }

    public IEnumerator RevivePlayer()
    {
        reviveUI.SetActive(false);
        player.transform.position = revivePoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        isReviving = false;
        isRevived = true;
    }
}
