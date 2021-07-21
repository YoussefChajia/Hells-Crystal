using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Text diamondsText;

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
        scoreText.text = "Score : " + score.ToString();
        diamondsText.text = "Diamonds : " + diamonds.ToString();
    }

    public void AddScore()
    {
        score += 1;
        diamonds += 1;
        scoreText.text = "Score : " + score.ToString();
    }

    private void OnApplicationQuit()
    {
        SaveSystem.SaveData(Player.instance);
    }
}
