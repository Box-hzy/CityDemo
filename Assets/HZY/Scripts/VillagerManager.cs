using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class VillagerManager : MonoBehaviour
{
    public static VillagerManager Instance;

    public GameObject prefab;
    public Transform normalVillagerParent;
    public Transform scaredVillagerParent;
    public Transform villagerGroup;
    public int numVillager = 15;
    BoxCollider spawnArea;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }
    private void Start()
    {
        spawnArea = GetComponent<BoxCollider>();


        for (int i = 0; i < numVillager; i++)
        {
            Vector3 randomPosition = SamplePositionOnNavMesh(GetRandomPosition(),100);
            Instantiate(prefab, randomPosition, Quaternion.identity, villagerGroup);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float y = Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y);
        float z = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
        return new Vector3(x, y, z);
    }

    public Vector3 SamplePositionOnNavMesh(Vector3 position, float distance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, distance, NavMesh.AllAreas))
        {
            position = hit.position;
        }
        return position;
    }

}
