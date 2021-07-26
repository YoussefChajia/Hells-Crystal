using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Revive UI")]
    [SerializeField] private GameObject reviveUI;
    [SerializeField] private Image uiCount;
    [SerializeField] private Button revive;
    [SerializeField] [Range(0f, 3f)] private float startWaitTime;
    [HideInInspector]
    public float waitTime;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    [Header("Revive Mechanic")]
    private bool isRevived;
    private bool isRevivable;


    #region Setters and Getters

    public GameObject getReviveUI()
    {
        return this.reviveUI;
    }

    public float getStartWaitTime()
    {
        return this.startWaitTime;
    }

    public void setIsRevived(bool isRevived)
    {
        this.isRevived = isRevived;
    }

    public void setIsRevivable(bool isRevivable)
    {
        this.isRevivable = isRevivable;
    }

    public bool getIsRevived()
    {
        return this.isRevived;
    }

    public bool getIsRevivable()
    {
        return this.isRevivable;
    }

    #endregion

    private void OnEnable()
    {
        GameEvents.current.onReviveButtonClick += OnPlayerRevive;
        GameEvents.current.onRespawnPlayerTrigger += OnPlayerRespawn;
    }

    private void Update()
    {
        if (isRevivable)
        {
            ShowReviveUI();
        }
    }

    private void ShowReviveUI()
    {
        reviveUI.SetActive(true);
        uiCount.fillAmount = Mathf.InverseLerp(0, 2f, waitTime);
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            waitTime = 0;
            isRevivable = false;
            GameEvents.current.RespawnPlayerTrigger();
        }
    }

    private void OnPlayerRevive()
    {
        isRevivable = false;
        reviveUI.SetActive(false);
        levelManager.HideLevelObjects(levelManager.getActiveLevel());
        StartCoroutine("RevivePlayer");
    }

    private IEnumerator RevivePlayer()
    {
        //yield return new WaitForSeconds(2f);
        Player.instance.transform.position = new Vector3(1.5f, Player.instance.transform.position.y - 10, 0);
        Player.instance.setIsDead(false);
        Player.instance.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(true);
        isRevived = true;
    }

    private void OnPlayerRespawn()
    {
        reviveUI.SetActive(false);
        StartCoroutine("RespawnPlayer");
    }

    private IEnumerator RespawnPlayer()
    {
        //yield return new WaitForSeconds(2f);
        levelManager.ReactivateLevelObjects(Player.instance.getLevelManager().getActiveLevel());
        Player.instance.transform.position = Player.instance.getLevelManager().getActiveLevel().getRespawnPoint().transform.position;
        Player.instance.setIsDead(false);
        Player.instance.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(true);
        isRevived = false;
    }

    private void OnDisable()
    {
        GameEvents.current.onReviveButtonClick -= OnPlayerRevive;
        GameEvents.current.onRespawnPlayerTrigger -= OnPlayerRespawn;
    }
}
