using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tabs;

    private void Start()
    {
        tabs[1].gameObject.SetActive(false);
    }

    public void OnButtonClick(int index)
    {
        HideTabs(index);
    }

    private void HideTabs(int index)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (i == index)
            {
                tabs[i].gameObject.SetActive(true);
            }
            else
            {
                tabs[i].gameObject.SetActive(false);
            }
        }
    }
}
