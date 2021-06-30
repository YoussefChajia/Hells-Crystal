using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRevive : MonoBehaviour
{
    public Player player;

    [Header("Revive UI")]
    public GameObject reviveUI;
    public Image uiCount;
    private float waitTime = 2f;

    private bool isRevived;

    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private GameObject revivePoint;

    private void Update()
    {
        if (player.isDead)
        {
            CheckRevive();
        }
    }

    public void CheckRevive()
    {
        StartCoroutine("CheckRevivePlayer");
    }

    public IEnumerator CheckRevivePlayer()
    {
        yield return new WaitForSeconds(0.75f);
        if (!isRevived)
        {
            ShowReviveUI();
        }
        else
        {
            Respawn();
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
            reviveUI.SetActive(false);
            Respawn();
        }
    }

    public void Revive()
    {
        waitTime = 0;
        reviveUI.SetActive(false);
        StartCoroutine("RevivePlayer");
    }

    public IEnumerator RevivePlayer()
    {
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        player.transform.position = revivePoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        isRevived = true;
    }

    public void Respawn()
    {
        StartCoroutine("RespawnPlayer");
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.75f);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(2f);
        player.transform.position = respawnPoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        isRevived = false;
    }

}
