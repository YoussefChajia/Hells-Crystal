using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image mask;

    private float distance;
    private float current;
    private Level activeLevel;

    private void Start()
    {
        activeLevel = Player.instance.getLevelManager().getActiveLevel();
        distance = activeLevel.getCheckPoint().transform.position.y - activeLevel.getRespawnPoint().transform.position.y;
    }

    private void Update()
    {
        GetFill();
    }

    private void GetFill()
    {
        current = activeLevel.getCheckPoint().transform.position.y - Player.instance.transform.position.y;
        mask.fillAmount = 1 - (current / distance);
    }
}
