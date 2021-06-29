using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    //public PlayerController player;
    public Player player;
    public GameObject respawnPoint;
    public Camera mainCamera;
    [HideInInspector]
    public bool isrevivable;
    public PlayerRevive revive;

    public GameObject[] revivePoints;

    public void Respawn()
    {
        StartCoroutine("RespawnPlayer");
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(0.75f);
        player.gameObject.SetActive(false);
        isrevivable = true;
        yield return new WaitForSeconds(2f);
        isrevivable = false;
        if (!revive.isReviving)
        {
            player.transform.position = respawnPoint.transform.position;
            player.isDead = false;
            player.isStop = true;
            yield return new WaitForSeconds(1.0f);
            player.gameObject.SetActive(true);
            revive.isRevived = false;
        }
    }
}
