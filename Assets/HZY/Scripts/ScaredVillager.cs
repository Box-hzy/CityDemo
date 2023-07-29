using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScaredVillager : VillagerBase
{
    //public Transform origin;
    float escapeRange = 200;
    //float angle = 30;
    float destroyTime = 20;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Invoke("DestroyVillager", destroyTime);
        SetEscapePoint();
      
    }

    public void SetEscapePoint()
    {
        //if (origin == null) return;
        //float randomAngle = Random.Range(-angle, angle);
        //Quaternion rotation = Quaternion.AngleAxis(randomAngle, Vector3.up);
        //Vector3 direction = rotation * origin.forward;
        //Vector3 direction = rotation * transform.forward;

        NavMeshHit hit;
        //NavMesh.SamplePosition(direction * escapeRange, out hit, escapeRange, NavMesh.AllAreas);
        NavMesh.SamplePosition(Random.insideUnitSphere * escapeRange, out hit, escapeRange, NavMesh.AllAreas);
        agent.SetDestination(hit.position);
        Debug.Log(hit.position);
    }

    void DestroyVillager()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

    }
}
