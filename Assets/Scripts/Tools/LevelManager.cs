using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player player;
    public Level[] levels;
    [HideInInspector] public Level activeLevel;

    public void HideLevelObjects(Level level)
    {
        HideObject(level.spikes);
        HideObject(level.blocks);
        HideObject(level.mechanics);
    }

    public void ReactivateLevelObjects(Level level)
    {
        ReactivateObject(level.spikes);
        ReactivateObject(level.blocks);
        ReactivateObject(level.mechanics);
    }

    private void HideObject(GameObject[] objectArray)
    {
        for (int i = 0; i < objectArray.Length; i++)
        {
            if (player.transform.position.y > objectArray[i].transform.position.y)
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
