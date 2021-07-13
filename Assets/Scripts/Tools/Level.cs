using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject spikesParent;
    [SerializeField] private GameObject blocksParent;
    [SerializeField] private GameObject mechanicsParent;

    [SerializeField] private GameObject[] spikes;
    [SerializeField] private GameObject[] blocks;
    [SerializeField] private GameObject[] mechanics;

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


    public void InitializeLevel()
    {
        spikes = GetArray(spikesParent);
        blocks = GetArray(blocksParent);
        mechanics = GetArray(mechanicsParent);
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
