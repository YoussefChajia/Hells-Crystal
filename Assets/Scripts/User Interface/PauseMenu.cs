using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    [SerializeField] private GameObject pauseMenu;

    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
