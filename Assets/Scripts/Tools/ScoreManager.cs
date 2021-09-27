using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text levelScore;
    [SerializeField] private Text currentLevel;
    [SerializeField] private Text levelCompletion;
    [SerializeField] private Text totalScore;
    [SerializeField] private ProgressBar progress;

    private int score;
    private int diamonds;

    public void setDiamonds(int diamonds)
    {
        this.diamonds = diamonds;
    }

    public int getDiamonds()
    {
        return this.diamonds;
    }

    public int getScore()
    {
        return this.score;
    }

    private void Start()
    {
        GameEvents.current.onPlayerRespawnTrigger += ResetScore;
        GameEvents.current.onDiamondTriggerEnter += AddScore;
        GameEvents.current.onPlayerQuitGame += QuitGame;
    }

    private void Update()
    {
        levelScore.text = score.ToString();
        currentLevel.text = "Current Level : " + Player.instance.getLevelManager().getActiveLevel().getlevelName();
        levelCompletion.text = "Level Completion : " + progress.getCompletion().ToString() + "%";
        totalScore.text = "Total Diamonds : " + diamonds.ToString();
    }

    private void AddScore()
    {
        score += 1;
        diamonds += 1;
        levelScore.text = score.ToString();
    }

    private void ResetScore()
    {
        score = 0;
        levelScore.text = score.ToString();
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
            SaveSystem.SaveData(Player.instance);
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(Player.instance);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerRespawnTrigger -= ResetScore;
        GameEvents.current.onDiamondTriggerEnter -= AddScore;
        GameEvents.current.onPlayerQuitGame -= QuitGame;
    }
}
