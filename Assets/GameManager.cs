using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CityGenerator cityGenerator;
    public NavMeshSurface[] navMeshSurfaces;


    public KeyCode generateButton = KeyCode.N;

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

            RefreshNavMeshSurface();

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
        
    }
}
