using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    GameObject closestHouse;
    //public GameObject[] buildings;

    public House[] houses;
    public List<House> accessibleHouses;

    float minDistance;
    
    //public Dictionary<GameObject,int> HouseState = new Dictionary<GameObject,int>(); //Housename,house state(0 for nothing, 1 for burning,2 for fire get extinguished)

    GameObject currentBuringHouse;
    GameManagement gameManagement;

    private void Start()
    {
        
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        accessibleHouses = new List<House>();
        //buildings = GameObject.FindGameObjectsWithTag("Buildings");
        AddHouses();
        AddaccessibleHouses();

    }

    void ClosestHouse(GameObject gameObject) {
        minDistance = Mathf.Infinity;
        closestHouse = null;

        

        foreach (House house in houses)
        {
            float distance = Vector3.Distance(gameObject.transform.position, house.getCentre());
            if (distance < minDistance)
            {
                closestHouse = house.gameObject;
                minDistance = distance;

            }
        }
    }
    
    public GameObject getClosestHouse(GameObject gameObject) //return a transform with the nearest house
    {
        ClosestHouse(gameObject);

        return closestHouse;
    }

    public float getMinDistance(GameObject gameObject) {
        ClosestHouse(gameObject);

        return minDistance;
    }



    public void setHouseState(GameObject house,int stateNum) {
        house.GetComponent<House>().setState(stateNum);
        
    }


    public void ClosestHouseWithState(GameObject gameObject,int state)
    {
        minDistance = Mathf.Infinity;
        closestHouse = null;


        foreach (House house in houses)
        {
            float distance = Vector3.Distance(gameObject.transform.position, house.getCentre());
            if (distance < minDistance && house.getState() == state)
            {
                closestHouse = house.gameObject;
                minDistance = distance;

            }
        }
  
    }


    public GameObject getClosestHouseWithState(GameObject gameObject, int state) //return a transform with the nearest house
    {
        ClosestHouseWithState(gameObject,state);

        return closestHouse;
    }

    public float getMinDistanceWithState(GameObject gameObject, int state)
    {
        ClosestHouseWithState(gameObject, state);

        return minDistance;
    }

    public void setCurrentBurningHouse(GameObject thisGamObject) {
        currentBuringHouse = thisGamObject;
    }

    public GameObject getCurrentBurningHouse() {
        return currentBuringHouse;
    }


    void AddHouses()    //add houses to the list
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Buildings");
        houses = new House[goArray.Length];
        for (int i = 0; i < goArray.Length; i++)
        {
            houses[i] = goArray[i].GetComponent<House>();
        }
    }

    void AddaccessibleHouses()
    {
        for (int i = 0; i < houses.Length; i++)
        {
            if(houses[i].transform.childCount >0)
           
            if (houses[i].transform.GetChild(0).CompareTag("EscapePoint"))
            {
                accessibleHouses.Add(houses[i]);
            }
        }
    }
    
}
