using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public npcFactory npcFactory;
    public int numOfNPC;

    //[HideInInspector]public List<Vector3> waypoints = new List<Vector3>();
    //public GameObject waypointPrefab;
    public Transform waypointGroup;


    public CityGenerator cityGenerator;
    public NavMeshSurface[] navMeshSurfaces;
    bool start;

    public Transform playerSpwanPt;
    public GameObject player;
    public bool activateLight;

    //public List<Transform> accessibleHouses = new List<Transform>();


    public KeyCode generateButton = KeyCode.N;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        npcFactory.range = 20;

        if (cityGenerator)
        {
            ClearhWaypoint();
            cityGenerator.GenerateCity();

            start =true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(generateButton))
        {
            if (cityGenerator)
            {
                ClearhWaypoint();
                cityGenerator.GenerateCity();

                start = true;

                player.transform.position = playerSpwanPt.position;


            }


        }

    }

    void ClearhWaypoint()
    {
        if (waypointGroup.childCount <= 0) return;
        for (int i = 0; i < waypointGroup.childCount; i++)
        {
            Destroy(waypointGroup.GetChild(i).gameObject);
        }
    }

    //void SpawnWaypoint()
    //{ 
    //    for(int i = 0;i < waypoints.Count;i++) 
    //    {
    //        Instantiate(waypointPrefab, waypoints[i], Quaternion.identity, waypointGroup);
    //    }
    //}

    void RefreshNavMeshSurface()
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            if (navMeshSurfaces[i] != null)
            {
                navMeshSurfaces[i].BuildNavMesh();
            }
        }

    }

    void RegenerateNPC()
    {
        //refresh nav mesh bounds
        //npcFactory.navMeshBounds = npcFactory.CalculateBoundsOfNavMesh();
        //npcFactory.RefreshRanges();

        //clear npc
        for (int i = 0; i < npcFactory.npcParent.childCount; i++)
        {
            Destroy(npcFactory.npcParent.GetChild(i).gameObject);
        }
        //generate npc
        for (int i = 0; i < numOfNPC; i++)
        {
            GameObject newNpc = npcFactory.CreateNpc();
            newNpc.name = newNpc.name + "_" + i.ToString();
        }
    }

    void Refresh()
    {
        RefreshNavMeshSurface();
        RegenerateNPC();
    }

    private void LateUpdate()
    {

        if (start)
        {
            Refresh();
            start = false;
        }

        //if (Input.GetKeyDown(generateButton))
        //{
        //    Refresh();
        //}

    }


}
