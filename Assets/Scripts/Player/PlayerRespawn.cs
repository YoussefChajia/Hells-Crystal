using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Mechanic")]
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
        Player.instance.transform.position = respawnPoint.transform.position;
        Player.instance.setIsDead(false);
        Player.instance.setIsStop(true);
        yield return new WaitForSeconds(1.0f);
        Player.instance.gameObject.SetActive(true);
        playerRevive.setIsRevived(false);
    }
}
