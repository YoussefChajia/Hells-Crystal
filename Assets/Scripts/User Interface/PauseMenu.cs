using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private void Start()
    {
        GameEvents.current.onPauseButtonClick += OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick += OnPauseMenuClose;
    }

    private void OnPauseMenuOpen()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnPauseMenuClose()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        GameEvents.current.onPauseButtonClick -= OnPauseMenuOpen;
        GameEvents.current.onResumeButtonClick -= OnPauseMenuClose;
    }
}
