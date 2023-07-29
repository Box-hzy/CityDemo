using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PickUpStick : MonoBehaviour
{
    [SerializeField]bool canPickUp;
    public bool isHoldingStick;
    public GameObject stick;

    float ActiveDuration;
    [SerializeField]float timer;

    GameManagement gameManagement;
    public VisualEffect TorchVFX;
    public ParticleSystem TorchParticle;
    public ParticleSystem SparkParticle;


    private void Start()
    {
        stick.SetActive(false);
        gameManagement = GameObject.Find("GameManagement").GetComponent<GameManagement>();
        ActiveDuration = gameManagement.getTorchFireActiveTime();
        
    }

    private void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("111");
                stick.SetActive(true);
                ActiveDuration = gameManagement.getTorchFireActiveTime();
                timer = ActiveDuration;
            }
        }


        if (stick.activeSelf)
        {
            TorchParticle.Play();
            SparkParticle.Play();
            timer -= Time.deltaTime;
            //float currentFireSize = (timer / ActiveDuration) * TorchVFX.GetFloat("MaxSize");
            float currentFireSize = (timer / ActiveDuration) * 15;
            if (timer <= 0)
            {
                stick.SetActive(false);
                //TorchVFX.SetFloat("FireSize", 0);
                TorchParticle.Stop();
                SparkParticle.Stop();
            }
            else
            {
                //TorchVFX.SetFloat("FireSize", currentFireSize);
                var emission = TorchParticle.emission;
                emission.rateOverTimeMultiplier = currentFireSize;
                var emission2 = SparkParticle.emission;
                emission2.rateOverTimeMultiplier = currentFireSize * 10 / 15;
            }

        }


        isHoldingStick = stick.activeSelf;

    }

    public void SetPickUp()
    {
        canPickUp = !canPickUp;
    }

    //public void getWeather(){
    //  
    //}

}
