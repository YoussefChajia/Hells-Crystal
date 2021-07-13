using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Mechanic")]
    [SerializeField] private Player player;
    [SerializeField] private GameObject respawnPoint;


    [Header("Revive Mechanic")]
    [SerializeField] private PlayerRevive playerRevive;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    public GameObject getRespawnPoint()
    {
        return this.respawnPoint;
    }

    public void Death()
    {
        playerRevive.waitTime = playerRevive.getStartWaitTime();
        StartCoroutine("PlayerDeath");
    }

    public IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(0.75f);
        player.gameObject.SetActive(false);

        if (!playerRevive.getIsRevived() && player.getIsDead())
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
        levelManager.ReactivateLevelObjects(player.getLevelManager().getActiveLevel());
        player.transform.position = respawnPoint.transform.position;
        player.setIsDead(false);
        player.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        playerRevive.setIsRevived(false);
    }
}
