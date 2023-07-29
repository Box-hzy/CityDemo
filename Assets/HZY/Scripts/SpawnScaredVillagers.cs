using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScaredVillagers : MonoBehaviour
{

    public GameObject scaredVillager;
    [SerializeField]public House thisHouse;
    [SerializeField]private Transform escapePoint;
    bool isSpawn;
    private void Awake()
    {
        escapePoint = transform.parent;
        //thisHouse = escapePoint.parent.GetComponent<House>();
        //if (transform.childCount > 0)
        //{
        //    if (transform.GetChild(0).CompareTag("EscapePoint"))
        //        escapePoint = transform.GetChild(0);
        //}
    }

    private void Update()
    {
        CheckState();
    }

    void CheckState()
    {
        switch (thisHouse.getState())
        {
            case 0:
                isSpawn = false;
                break;
            case 1:
                if (!isSpawn)
                {
                    StartCoroutine(Spawn());
                }
                break;
        }
    }

    IEnumerator Spawn()
    {
        if (escapePoint == null) yield break;
        isSpawn = true;
        Debug.Log("spawn scared villagers");
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(scaredVillager, escapePoint.position, Quaternion.identity, 
                VillagerManager.Instance.scaredVillagerParent);
            go.transform.forward = escapePoint.forward;
            yield return new WaitForSeconds(0.8f);
            //go.GetComponent<ScaredVillager>().origin = escapePoint;
            //go.GetComponent<ScaredVillager>().SetEscapePoint();
        }

    }
}
