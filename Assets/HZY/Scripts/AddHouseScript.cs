using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class AddHouseScript : MonoBehaviour
{
    public Vector2Int randomScore;
    public Material burningMat;
    public Material destoryMat;
    public GameObject firePrefab;
    public GameObject spawnScaredVillagerScriptPrefab;

    private void Awake()
    {

        int childCout = transform.childCount;

        for (int i = 0; i < childCout; i++)
        {
            if (transform.GetChild(i).GetComponent<MeshRenderer>() == null) return; //ignore EscapePoint empty obj

            Transform child = transform.GetChild(i);

            //child.AddComponent<SpawnScaredVillagers>();

            child.tag = "Buildings";

            //add script with prefab
            if (child.childCount > 0)
            {
                if (child.GetChild(0).CompareTag("EscapePoint"))
                {
                    Instantiate(spawnScaredVillagerScriptPrefab, child.GetChild(0).position, Quaternion.identity, child.GetChild(0));
                }
            }

            Vector3 center = child.GetComponent<MeshRenderer>().bounds.center;
            GameObject fire =  Instantiate(firePrefab, center, Quaternion.identity,child);
            fire.transform.forward = Vector3.up;

            var house = child.AddComponent<House>();

            //set score
            int random = Random.Range(randomScore.x, randomScore.y);
            house.setScore(random);



            //if (transform.CompareTag("BigHouse"))
            //{
            //    house.setScore(200);
            //}
            //else if (transform.CompareTag("SmallHouse"))
            //{
            //    house.setScore(100);
            //}
        }

    }


}
