using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PoliceCar : MonoBehaviour
{
    public Vector3 firehouseDestination;
    public Vector3 PoliceStationDestination;
    public GameObject firehouse;
    float stopDistance;
    public int state = 0;
    public NavMeshAgent navMeshAgent;
    public PolicemanInvInCar policemanInv;
    public PolicemanPatrolInCar policemanPatrol;
    public GameObject policemanInv_GO;
    public GameObject policemanPatrol_GO;

    GameManagement gameManagement;
    // Start is called before the first frame update
    void Start()
    {
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        //policemanInv = GetComponentInChildren<PolicemanInvInCar>();
        //policemanPatrol = GetComponentInChildren<PolicemanPatrolInCar>();
        policemanPatrol.setPolicemanInv(policemanInv);
        navMeshAgent= GetComponent<NavMeshAgent>();
        navMeshAgent.speed = gameManagement.getPoliceCarSpeed();
        stopDistance = gameManagement.getPoliceCarStopDistance();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector3.Distance(transform.position, firehouseDestination));
        switch (state) {
            
            case 0:
                NavMeshHit navHit;
                NavMesh.SamplePosition(firehouseDestination, out navHit, 10, NavMesh.AllAreas);
                navMeshAgent.SetDestination(navHit.position);
                if (Vector3.Distance(transform.position, firehouseDestination) <= stopDistance)
                //if (navMeshAgent.remainingDistance < 0)
                {
                    //Debug.Log("2323");
                    navMeshAgent.SetDestination(transform.position);
                    navMeshAgent.isStopped = true;

                    policemanPatrol_GO.SetActive(true);
                    policemanPatrol.SetPoliceCar(transform.position);


                    policemanInv_GO.SetActive(true);
                    policemanInv.SetPoliceCar(transform.position);
                    changeState(1);
                }

                break;
            case 1:
                if (policemanInv.getBackToCar() && policemanPatrol.getBackToCar()) {
                    changeState(2);
                }
                break;
            case 2:
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(PoliceStationDestination);
                if (Vector3.Distance(transform.position, PoliceStationDestination) <= 1.0f)
                {
                    Destroy(this.gameObject);
                }
                break;

        }
    }

    public void changeState(int thisState)
    {
        state = thisState;
    }

    public void setPolicehouseDestination(Vector3 thisFirehouse)
    {
        firehouseDestination = thisFirehouse;
    }
    public void setPoliceStationDestination(Vector3 thisFireStation)
    {
        PoliceStationDestination = thisFireStation;
    }
}
