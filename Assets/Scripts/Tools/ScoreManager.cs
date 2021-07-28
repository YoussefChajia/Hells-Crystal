using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;

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
        scoreText.text = "Score : " + score.ToString();
    }

    private void AddScore()
    {
        score += 1;
        diamonds += 1;
        scoreText.text = "Score : " + score.ToString();
    }

    private void ResetScore()
    {
        score = 0;
        scoreText.text = "Score : " + score.ToString();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(Player.instance);
    }

    private void OnDestroy()
    {
        GameEvents.current.onPlayerRespawnTrigger -= ResetScore;
        GameEvents.current.onDiamondTriggerEnter -= AddScore;
    }
}
