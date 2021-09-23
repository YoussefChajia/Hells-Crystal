using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    private float distance;
    private float current;
    private Level activeLevel;

    //Create a an event for level finish and put the first two lignes in a function for it and also add functions related to level finish
    private void Update()
    {
        activeLevel = Player.instance.getLevelManager().getActiveLevel();
        distance = activeLevel.getCheckPoint().transform.position.y - activeLevel.getRespawnPoint().transform.position.y;
        current = activeLevel.getCheckPoint().transform.position.y - Player.instance.transform.position.y;
        mask.fillAmount = 1 - (current / distance);
    }
    public int getCompletion()
    {
        int completion = ((int)Mathf.Round((1 - (current / distance)) * 100));
        return completion;
    }
}
