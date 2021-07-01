using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Player player;

    [Header("Player Respawn")]
    public PlayerRespawn levelManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            player.isDead = true;
            player.animator.SetTrigger("isDead");
            Death();
        }
    }

    public void Death()
    {
        StartCoroutine("FakeDeath");
    }

    public IEnumerator FakeDeath()
    {
        yield return new WaitForSeconds(0.75f);
        player.gameObject.SetActive(false);
        levelManager.CheckRevive();
    }
}
