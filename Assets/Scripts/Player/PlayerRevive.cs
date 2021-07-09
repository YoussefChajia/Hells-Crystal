using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRevive : MonoBehaviour
{
    public Player player;
    public PlayerRespawn levelManager;

    [Header("Revive UI")]
    public GameObject reviveUI;
    [SerializeField] private Image uiCount;
    [SerializeField] private Button revive;
    [Range(0f, 3f)] public float startWaitTime;
    [HideInInspector]
    public float waitTime;

    [Header("Revive Mechanic")]
    [SerializeField] private GameObject revivePoint;
    [HideInInspector]
    public bool isRevived;
    [HideInInspector]
    public bool isRevivable;

    private void Update()
    {
        if (isRevivable)
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
            isRevivable = false;
            levelManager.Respawn();
        }
    }

    public void Revive()
    {
        isRevivable = false;
        reviveUI.SetActive(false);
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
