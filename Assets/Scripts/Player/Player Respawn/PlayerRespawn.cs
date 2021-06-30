using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Player player;
    public GameObject respawnPoint;
    public Camera mainCamera;

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
    }
}
