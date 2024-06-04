using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ohjh9901_PullManager : MonoBehaviour
{
    public static Ohjh9901_PullManager pullManager;
    public GameObject[] bullets;
    List<GameObject>[] pools;

    void Awake()
    {
        pullManager = this;
        pools = new List<GameObject>[bullets.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(select == null)
        {
            select = Instantiate(bullets[index], transform);
            pools[index].Add(select);
        }

        return select;
    }
    
}
