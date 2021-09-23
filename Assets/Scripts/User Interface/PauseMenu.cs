using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("Magic")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject scoreText;

    [Header("Selection")]
    [SerializeField] private GameObject[] tabs;
    [SerializeField] private Image[] panels;
    [SerializeField] private Text[] texts;

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
        scoreText.SetActive(false);
        Time.timeScale = 0f;
        Player.instance.setIsPaused(true);
    }

    private void OnPauseMenuClose()
    {
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        scoreText.SetActive(true);
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

        foreach (var item in texts)
            item.color = Color.grey;

        tabs[index].SetActive(true);
        panels[index].color = Color.white;
        texts[index].color = Color.white;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPauseButtonClick -= OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick -= OnPauseMenuClose;
    }
}
