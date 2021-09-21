using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject resumeButton;

    [SerializeField] private GameObject[] tabs;
    [SerializeField] private Image[] panels;

    private void Start()
    {
        pauseMenu.SetActive(false);
        GameEvents.current.onPauseButtonClick += OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick += OnPauseMenuClose;
    }

    private void OnPauseMenuOpen()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        Time.timeScale = 0f;
        Player.instance.setIsPaused(true);
    }

    private void OnPauseMenuClose()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        Time.timeScale = 1f;
        Player.instance.setIsPaused(false);
        onButtonClick(0);
    }

    public void onButtonClick(int index)
    {
        foreach (var item in tabs)
            item.SetActive(false);
        foreach (var item in panels)
            item.color = Color.grey;

        tabs[index].SetActive(true);
        panels[index].color = Color.white;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPauseButtonClick -= OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick -= OnPauseMenuClose;
    }
}
