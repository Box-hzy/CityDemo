using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FireEngine : MonoBehaviour
{
    public GameObject Fireman;
    public  NavMeshAgent navMeshAgent;
    FiremanScript firemanScript;
    public Vector3 firehouseDestination;
    public GameObject fireStationDestination;
    public GameObject firehouse;
    float stopDistance;
    GameManagement gameManagement;
    public int state = 0;//0: go to firehouse, 1:arrived , 2: back
    FireStationManagement fireStationManagement;
    GameObject firestation;
    // Start is called before the first frame update
    void Start()
    {
        
        Fireman = transform.GetChild(0).gameObject;
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        firemanScript = Fireman.GetComponent<FiremanScript>();
        firemanScript.ClosestFireHouse = firehouse;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = gameManagement.getFireEngineSpeed();
        stopDistance = gameManagement.getFireTruckStopDistance();
        fireStationManagement = GameObject.Find("FireStationManagement").GetComponent<FireStationManagement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Vector3.Distance(transform.position, firehouseDestination));
        switch (state)
        {
            case 0:
                NavMeshHit navHit;
                NavMesh.SamplePosition(firehouseDestination, out navHit, 10, NavMesh.AllAreas);
                navMeshAgent.SetDestination(navHit.position);

                if (Vector3.Distance(transform.position, firehouseDestination) <= stopDistance)
                {
                    //Debug.Log("222");
                    navMeshAgent.isStopped = true;
                    Fireman.SetActive(true);
                    Fireman.transform.SetParent(null);
                    firemanScript.SetFireEnginePostion(transform.position);
                    changeState(1);
                }

                break;
            case 1:
                break;
            case 2:
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(fireStationDestination.transform.position);
                if (Vector3.Distance(transform.position, fireStationDestination.transform.position) <= 1.0f)
                {
                    fireStationManagement.changeFireEngineNumber(firestation, 1);
                    Destroy(this.gameObject);
                    
                }
                break;
        }
    }

    public void changeState(int thisState) { 
        state = thisState; 
    }

    public void setFirehouseDestination(Vector3 thisFirehouse) {
        firehouseDestination = thisFirehouse;
    }
    public void setFireStationDestination(GameObject thisFireStation)
    {
        fireStationDestination = thisFireStation;
    }

    public void setFireStation(GameObject thisFireStation) { 
        firestation= thisFireStation;
    }

    
}
