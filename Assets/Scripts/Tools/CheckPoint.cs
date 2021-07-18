using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Level previousLevel;
    [SerializeField] private Level nextLevel;

    public Level getPreviousLevel()
    {
        return this.previousLevel;
    }

    public Level getNextLevel()
    {
        return this.nextLevel;
    }
}
