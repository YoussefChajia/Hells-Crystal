using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    private Level activeLevel;
    private int reachedLevel;

    public void setActiveLevel(Level activeLevel)
    {
        this.activeLevel = activeLevel;
    }

    public Level getActiveLevel()
    {
        return this.activeLevel;
    }

    public Level[] getLevels()
    {
        return this.levels;
    }

    public void setReachedLevel(int reachedLevel)
    {
        this.reachedLevel = reachedLevel;
    }

    public int getReachedLevel()
    {
        return this.reachedLevel;
    }

    public void HideLevelObjects(Level level)
    {
        HideObject(level.getSpikes());
        HideObject(level.getBlocks());
        HideObject(level.getMechanics());
        HideObject(level.getDiamonds());
    }

    public void ReactivateLevelObjects(Level level)
    {
        ReactivateObject(level.getSpikes());
        ReactivateObject(level.getBlocks());
        ReactivateObject(level.getMechanics());
        ReactivateObject(level.getDiamonds());
    }

    private void HideObject(GameObject[] objectArray)
    {
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (Player.instance.transform.position.y > objectArray[i].transform.position.y + 6.0f)
            {
                objectArray[i].SetActive(false);
            }
        }
    }

    private void ReactivateObject(GameObject[] objectArray)
    {
        for (int i = 0; i < objectArray.Length; i++)
        {
            objectArray[i].SetActive(true);
        }
    }

    //This method activates the level after the checkpoint and diactivates the previous level
    public void ActivateLevel(CheckPoint checkPoint)
    {
        checkPoint.getPreviousLevel().gameObject.SetActive(false);
        checkPoint.getNextLevel().gameObject.SetActive(true);
        activeLevel = checkPoint.getNextLevel();
        activeLevel.InitializeLevel();
    }
}
