using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //public PlayerController player;
    public Player player;
    public GameObject respawnPoint;
    public Camera mainCamera;

    [Header("Revive Mechanic")]
    public PlayerRevive playerRevive;
    public bool isRevivable;

    public void CheckRevive()
    {
        if (!playerRevive.isRevived)
        {
            isRevivable = true;
        }
        else
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        playerRevive.reviveUI.SetActive(false);
        playerRevive.waitTime = 2f;
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
