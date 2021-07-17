using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levels;
    private Level activeLevel;

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

    public void HideLevelObjects(Level level)
    {
        HideObject(level.getSpikes());
        HideObject(level.getBlocks());
        HideObject(level.getMechanics());
    }

    public void ReactivateLevelObjects(Level level)
    {
        ReactivateObject(level.getSpikes());
        ReactivateObject(level.getBlocks());
        ReactivateObject(level.getMechanics());
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
    public void ActivateLevel(int checkpoint)
    {
        levels[checkpoint].gameObject.SetActive(false);
        levels[checkpoint + 1].gameObject.SetActive(true);
        activeLevel = levels[checkpoint + 1];
        activeLevel.InitializeLevel();
    }
}
