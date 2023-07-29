using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerBase : MonoBehaviour
{

    protected void Awake()
    {
        ChangeSkin();
    }


    void ChangeSkin()
    {
        List<GameObject> list = new List<GameObject>();
        int childCount = transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SkinnedMeshRenderer>(out SkinnedMeshRenderer mesh))
            {
                list.Add(transform.GetChild(i).gameObject);
            }
        }

        int randomSkinIndex = Random.Range(0, list.Count);

        for (int i = 0; i < list.Count; i++)
        {
            if (i == randomSkinIndex)
            {
                list[i].SetActive(true);
            }
            else
            {
                list[i].SetActive(false);
            }
        }
    }
}
