//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.SceneManagement;


//public class PolicemanPatrolInCar : MonoBehaviour
//{
//    public Material material;
//    Color green = new Color(0, 1, 0, 0.5f);
//    Color red = new Color(1, 0, 0, 0.5f);
//    GameObject player;
//    Vector3 PolicecarVector3;
//    float patrolSpeed;
//    float chaseSpeed;
//    public enum State { 
//        Patrol,
//        Chase,
//        BackToCar
//    }
//    public State policeState;

//    public NavMeshAgent agent;
//    public float maxPatrolDistance;
//    public float viewDistance = 10f; // view distance
//    public float viewAngle = 90f; // view angle
//    public float foggyViewDistance = 6f;
//    public float sunnyViewDistance = 10f;

//    public List<Collider> targetsInViewRadius = new List<Collider>();
//    public MeshFilter meshFilter;
//    Mesh mesh;

//    PolicemanInvInCar policemanInv;
//    public bool backToCar = false;

//    float PoliceTime;
//    public float thisTime;

//    GameManagement gameManagement;
//    // Start is called before the first frame update
//    void Start()
//    {
//        player = GameObject.FindWithTag("Player");
//        mesh = CreateMesh();
//        meshFilter.mesh = mesh;
//        agent = GetComponent<NavMeshAgent>();
//        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
//        patrolSpeed = gameManagement.getPolicePatrolSpeed();
//        chaseSpeed = gameManagement.getPoliceChaseSpeed();
//        PoliceTime = gameManagement.getPoliceTime();
//        thisTime = PoliceTime;
//        material.SetColor("_BaseColor", green);
//    }
//    private void Awake()
//    {
//        backToCar = false;
        
        
//    }

//    private void OnEnable()
//    {
//        policeState = State.Patrol;
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        targetsInViewRadius = new List<Collider>(Physics.OverlapSphere(transform.position, viewDistance));
//        thisTime -= Time.deltaTime;

//        //if (Input.GetKeyDown(KeyCode.P))
//        //{
//        //    SceneManager.LoadScene(0);
//        //}

       

//        switch (policeState)
//        {
//            case State.Patrol:
//                Patrol();
//                break;
//            case State.Chase:
//                Chase();
//                break;
//            case State.BackToCar:
//                agent.SetDestination(PolicecarVector3);
//                if (Vector3.Distance(transform.position, PolicecarVector3) <= 2.0f) { 
//                    backToCar= true;
//                    gameObject.SetActive(false);
//                }
//                break;
//        }

//        if (targetsInViewRadius.Count > 0)
//        {
//            if (targetsInViewRadius.Contains(player.GetComponent<CapsuleCollider>()))
//            {

//                Vector3 dirToTarget = (player.transform.position - transform.position).normalized;
//                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2f)
//                {
//                    policeState = State.Chase;
//                    material.SetColor("_BaseColor", red);

//                    //Debug.Log("Player is in view!");
//                }
//                else if (thisTime <= 0)
//                {
//                    policeState = State.BackToCar;
//                    material.SetColor("_BaseColor", green);

//                }
//                else {
//                    policeState = State.Patrol;
//                    material.SetColor("_BaseColor", green);
//                }
//            }
//            else if (thisTime <= 0)
//            {
//                policeState = State.BackToCar;
//                material.SetColor("_BaseColor", green);

//            }
//            else
//            {
//                policeState = State.Patrol;
//                material.SetColor("_BaseColor", green);
//            }


//        }
//    }


//    void Chase()
//    {
//        agent.SetDestination(player.transform.position);
//        agent.speed = chaseSpeed;
//        Debug.Log(Vector3.Distance(transform.position, player.transform.position));
//        if (Vector3.Distance(transform.position, player.transform.position) <= 3) {
//            //SceneManager.LoadScene("Level1");
//            UImanager.instance.ShowResult();
//            gameManagement.canRestart = true;

//        }
//    }

//    void Patrol()
//    {
//        agent.speed = patrolSpeed;
//        if (!agent.pathPending && agent.remainingDistance < 0.5f)//if police is not walking
//        {
//            Vector3 randomPoint = RandomNavmeshLocation(transform.position, maxPatrolDistance);
//            agent.speed = patrolSpeed;
//            agent.SetDestination(randomPoint);
//            StartCoroutine(StopForSeconds(2));
//        }
//    }



//    private Vector3 RandomNavmeshLocation(Vector3 origin, float distance)
//    {
//        // Random generate a new location
//        Vector3 randomDirection = Random.insideUnitSphere * distance;
//        randomDirection += origin;
//        NavMeshHit navHit;
//        NavMesh.SamplePosition(randomDirection, out navHit, distance, NavMesh.AllAreas);
//        return navHit.position;
//    }

//    private IEnumerator StopForSeconds(float seconds)
//    {
//        // stop moving for seconds.
//        agent.isStopped = true;
//        yield return new WaitForSeconds(seconds);
//        agent.isStopped = false;

//    }


//    private Mesh CreateMesh()
//    {
//        Mesh mesh = new Mesh();
//        int vertexCount = 16;
//        Vector3[] vertices = new Vector3[vertexCount + 1];
//        int[] triangles = new int[vertexCount * 3];

//        vertices[0] = Vector3.zero;
//        float angleStep = viewAngle / (vertexCount - 1);
//        for (int i = 0; i < vertexCount; i++)
//        {
//            float angle = -viewAngle / 2f + angleStep * i;
//            float x = Mathf.Sin(angle * Mathf.Deg2Rad);
//            float z = Mathf.Cos(angle * Mathf.Deg2Rad);
//            vertices[i + 1] = new Vector3(x, 0f, z) * viewDistance;
//        }

//        for (int i = 0; i < vertexCount - 1; i++)
//        {
//            triangles[i * 3] = 0;
//            triangles[i * 3 + 1] = i + 1;
//            triangles[i * 3 + 2] = i + 2;
//        }

//        mesh.vertices = vertices;
//        mesh.triangles = triangles;
//        mesh.RecalculateNormals();
//        return mesh;
//    }

//    public void SetPoliceCar(Vector3 vector3)
//    {
//        PolicecarVector3 = vector3;
//    }

//    public bool getBackToCar() { 
//        return backToCar;
//    }

//    public void setPolicemanInv(PolicemanInvInCar thisPolice) { 
//        policemanInv= thisPolice;
//    }

//    public State getState() {
//        return policeState;
//    }

    
//}
