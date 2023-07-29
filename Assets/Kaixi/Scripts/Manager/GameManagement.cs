using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagement : MonoBehaviour
{
    [Header("Alarm")]
    [SerializeField] bool FireAlarm = false;
    [SerializeField] bool PoliceAlarm = false;

    [Header("Torch")]
    [SerializeField] float TorchFireActiveTime_Noramal;
    [SerializeField] float TorchFireActiveTime_Rainy;

    [Header("Player")]
    [SerializeField] float PlayerSpeed;

    [Header("Fire")]
    [SerializeField] float smallFireIncreaseSpeed;
    [SerializeField] float largeFireIncreaseSpeed;
    [SerializeField] float oldFireIncreaseSpeed;
    [SerializeField] float FireSpreadTime;

    [Header("Fireman")]
    [SerializeField] [Range(1,200)]float FiremanPutOffFireSpeed;
    [SerializeField] float FiremanMovingSpeed;
    [SerializeField] float FiremanRange;

    [Header("FireTruck")]
    [SerializeField] float FireTruckSpeed;
    [SerializeField] float FireTruckStopDistance;

    [Header("Policeman")]
    [SerializeField] float PolicePatrolSpeed;
    [SerializeField] float PoliceChaseSpeed;
    [SerializeField] float PoliceTime;

    [Header("PoliceCar")]
    [SerializeField] float PoliceCarSpeed;
    [SerializeField] float PoliceCarStopDistance;

    [Header("House")]
    [SerializeField] Material BurningMaterial;
    [SerializeField] Material DestroiedMaterial;
    [SerializeField] float HouseRecoverTime;

    WeatherManagement weatherManagement;
    WeatherManagement.weatherType weatherType;

    public bool canRestart;

    private void Start()
    {
        weatherManagement = GameObject.Find("WeatherManagement").GetComponent<WeatherManagement>();
        weatherType = weatherManagement.getWeather();
        BurningMaterial = GetComponent<MeshRenderer>().materials[0];
        BurningMaterial = new Material(BurningMaterial);
        DestroiedMaterial = GetComponent<MeshRenderer>().materials[1];
        DestroiedMaterial = new Material(DestroiedMaterial);


    }

    private void Update()
    {
        weatherType = weatherManagement.getWeather();
        //Debug.Log(weatherManagement.getWeather());

        if (canRestart)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                SceneManager.LoadScene(0);
            }
        }
    }




    public bool getFireAlarm()
    {
        return FireAlarm;
    }

    public void setFireAlarm(bool state)
    {
        FireAlarm = state;
    }


    public bool getPoliceAlarm()
    {
        return PoliceAlarm;
    }

    public void setPoliceAlarm(bool state)
    {
        PoliceAlarm = state;
    }

    public float getTorchFireActiveTime()
    {
        if (weatherType == WeatherManagement.weatherType.Rainy) {
            return TorchFireActiveTime_Rainy;
        }
        return TorchFireActiveTime_Noramal;

        
    }

    public float getFiremanPutOffFireSpeed()
    {
        return FiremanPutOffFireSpeed/100;
    }

    public float getSmallFireIncreaseSpeed()
    {
        return smallFireIncreaseSpeed;
    }


    public float getLargeFireIncreaseSpeed()
    {
        return largeFireIncreaseSpeed;
    }

    public float getOldFireIncreaseSpeed()
    {
        return oldFireIncreaseSpeed;
    }

    public float getFireSpreadTime()
    {
        return FireSpreadTime;
    }

    public float getFiremanMovingSpeed()
    { 
        return FiremanMovingSpeed;
    }

    public float getFireEngineSpeed()
    {
        return FireTruckSpeed;
    }

    public float getPlayerSpeed()
    { 
        return PlayerSpeed;
    }

    public float getPolicePatrolSpeed()
    {
        return PolicePatrolSpeed;
    }

    public float getPoliceChaseSpeed() { 
        return PoliceChaseSpeed;
    }

    public float getPoliceCarSpeed() {
        return PoliceCarSpeed;
    }

    public float getFiremanRange(){
        return FiremanRange;
    }

    public float getFireTruckStopDistance() { 
        return FireTruckStopDistance;
    }

    public float getPoliceCarStopDistance() {
        return PoliceCarStopDistance;
    }

    public Material getBurningMaterial()
    {
        return BurningMaterial;
    }

    public Material getDestroiedMaterial() {
        return DestroiedMaterial;
    }

    public float getHouseRecoverTime() { 
        return HouseRecoverTime;
    }

    public float getPoliceTime() {
        return PoliceTime;
    }
}
