using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;



    public CityGenerator cityGenerator;
    public NavMeshSurface[] navMeshSurfaces;
    bool isBaked;

    public List<Transform> accessibleHouses = new List<Transform>();


    public KeyCode generateButton = KeyCode.N;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cityGenerator && cityGenerator.transform.childCount == 0)
        {
            cityGenerator.GenerateCity();
            RefreshNavMeshSurface();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(generateButton))
        {
            if (cityGenerator)
                cityGenerator.GenerateCity();

            //RefreshNavMeshSurface();

        }

    }

    void RefreshNavMeshSurface()
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            if (navMeshSurfaces[i] != null)
            {
                navMeshSurfaces[i].BuildNavMesh();
            } 
        }

        isBaked = true;

    }

    private void LateUpdate()
    {
        RefreshNavMeshSurface();
        
    }


}
