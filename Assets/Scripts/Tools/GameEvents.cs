using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    public event Action onPauseButtonClick;
    public void PauseButtonClick()
    {
        if (onPauseButtonClick != null)
        {
            onPauseButtonClick();
        }
    }

    public event Action onResumeButtonClick;
    public void ResumeButtonClick()
    {
        if (onResumeButtonClick != null)
        {
            onResumeButtonClick();
        }
    }

    public event Action onPlayerDeathTrigger;
    public void PlayerDeathTrigger()
    {
        if (onPlayerDeathTrigger != null)
        {
            onPlayerDeathTrigger();
        }
    }

    public event Action onPlayerRespawnTrigger;
    public void PlayerRespawnTrigger()
    {
        if (onPlayerRespawnTrigger != null)
        {
            onPlayerRespawnTrigger();
        }
    }

    public event Action onDiamondTriggerEnter;
    public void DiamondTriggerEnter()
    {
        if (onDiamondTriggerEnter != null)
        {
            onDiamondTriggerEnter();
        }
    }
}
