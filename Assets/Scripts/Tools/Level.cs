using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject spikesParent;
    [SerializeField] private GameObject blocksParent;
    [SerializeField] private GameObject mechanicsParent;

    public GameObject[] spikes;
    public GameObject[] blocks;
    public GameObject[] mechanics;


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
