using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject spikesParent;
    [SerializeField] private GameObject blocksParent;
    [SerializeField] private GameObject mechanicsParent;
    [SerializeField] private GameObject diamondsParent;

    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject[] mechanics;
    [SerializeField] private GameObject[] diamonds;

    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private GameObject checkPoint;

    public GameObject[] getSpikes()
    {
        return this.spikes;
    }

    public GameObject[] getBlocks()
    {
        return this.blocks;
    }

    public GameObject[] getMechanics()
    {
        return this.mechanics;
    }

    public GameObject[] getDiamonds()
    {
        return this.diamonds;
    }

    public GameObject getRespawnPoint()
    {
        return this.respawnPoint;
    }

    public GameObject getCheckPoint()
    {
        return this.checkPoint;
    }


    public void InitializeLevel()
    {
        spikes = GetArray(spikesParent);
        blocks = GetArray(blocksParent);
        mechanics = GetArray(mechanicsParent);
        diamonds = GetArray(diamondsParent);
    }

    private GameObject[] GetArray(GameObject parent)
    {
        GameObject[] array = new GameObject[parent.transform.childCount];

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            array[i] = parent.transform.GetChild(i).gameObject;
        }

        return array;
    }

}
