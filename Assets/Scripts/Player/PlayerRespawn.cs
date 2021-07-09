using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //public PlayerController player;
    public Player player;
    public GameObject respawnPoint;

    [Header("Revive Mechanic")]
    public PlayerRevive playerRevive;


    public void Death()
    {
        playerRevive.waitTime = playerRevive.startWaitTime;
        StartCoroutine("PlayerDeath");
    }

    public IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(0.75f);
        player.gameObject.SetActive(false);

        if (!playerRevive.isRevived && player.isDead)
        {
            playerRevive.isRevivable = true;
        }
        else
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        playerRevive.reviveUI.SetActive(false);
        StartCoroutine("RespawnPlayer");
    }

    public IEnumerator RespawnPlayer()
    {
        //yield return new WaitForSeconds(2f);
        player.transform.position = respawnPoint.transform.position;
        player.isDead = false;
        player.isStop = true;
        yield return new WaitForSeconds(1.0f);
        player.gameObject.SetActive(true);
        playerRevive.isRevived = false;
    }
}
