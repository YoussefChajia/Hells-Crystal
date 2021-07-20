
[System.Serializable]

public class GameData
{
    private int level;
    private int diamonds;

    public int getLevel()
    {
        return this.level;
    }

    public int getDiamonds()
    {
        return this.diamonds;
    }

    public GameData(Player player)
    {
        this.level = player.getLevelManager().getReachedLevel();
        this.diamonds = player.getScoreManager().getDiamonds();
    }
}
