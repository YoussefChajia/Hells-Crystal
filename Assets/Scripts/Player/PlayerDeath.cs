using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [Header("Player Respawn")]
    [SerializeField] private PlayerRespawn playerRespawn;

    [Header("Level Manager")]
    [SerializeField] private LevelManager levelManager;

    public void Death()
    {
        playerRespawn.waitTime = playerRespawn.getStartWaitTime();
        StartCoroutine("CheckDeath");
    }

    private IEnumerator CheckDeath()
    {
        yield return new WaitForSeconds(0.75f);
        Player.instance.gameObject.SetActive(false);

        if (!playerRespawn.getIsRevived())
        {
            playerRespawn.setIsRevivable(true);
        }
        else
        {
            GameEvents.current.RespawnPlayerTrigger();
        }
    }
}
