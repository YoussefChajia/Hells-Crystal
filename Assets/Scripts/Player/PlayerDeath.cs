using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Revive Mechanic")]
    [SerializeField] private PlayerRespawn playerRevive;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    private void Start()
    {
        GameEvents.current.onPlayerDeathTrigger += OnPlayerDeath;
    }

    public void OnPlayerDeath()
    {
        playerRevive.waitTime = playerRevive.getStartWaitTime();
        StartCoroutine("Death");
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(0.75f);
        Player.instance.gameObject.SetActive(false);

        if (!playerRevive.getIsRevived() && Player.instance.getIsDead())
        {
            playerRevive.setIsRevivable(true);
        }
        else
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        playerRevive.getReviveUI().SetActive(false);
        StartCoroutine("RespawnPlayer");
    }

    public IEnumerator RespawnPlayer()
    {
        //yield return new WaitForSeconds(2f);
        levelManager.ReactivateLevelObjects(Player.instance.getLevelManager().getActiveLevel());
        Player.instance.transform.position = Player.instance.getLevelManager().getActiveLevel().getRespawnPoint().transform.position;
        Player.instance.setIsDead(false);
        Player.instance.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(true);
        playerRevive.setIsRevived(false);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerDeathTrigger -= OnPlayerDeath;
    }
}
