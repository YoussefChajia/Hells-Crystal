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

    public event Action onReviveButtonClick;
    public void ReviveButtonClick()
    {
        if (onReviveButtonClick != null)
        {
            onReviveButtonClick();
        }
    }

    public event Action onRespawnPlayerTrigger;
    public void RespawnPlayerTrigger()
    {
        if (onRespawnPlayerTrigger != null)
        {
            onRespawnPlayerTrigger();
        }
    }
}
