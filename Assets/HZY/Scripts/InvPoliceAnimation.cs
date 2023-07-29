using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvPoliceAnimation : MonoBehaviour
{
    PolicemanInvInCar policemanInvInCar;
    Animator animator;
    PolicemanInvInCar.State currentState;

    // Start is called before the first frame update
    void Start()
    {
        policemanInvInCar = GetComponentInParent<PolicemanInvInCar>();
        animator = GetComponent<Animator>();
        currentState = policemanInvInCar.getState();
    }

    // Update is called once per frame
    void Update()
    {
        StateAnimation();
    }

    void StateAnimation()
    {
        currentState = policemanInvInCar.getState();

        if (currentState != PolicemanInvInCar.State.Investigate)
        {
            animator.SetBool("Text", false);
        } 
        else if (currentState != PolicemanInvInCar.State.FindVillager && currentState != PolicemanInvInCar.State.BackToCar)
        {
            animator.SetBool("Walk", false);
        }

        switch (currentState)
        {
            case PolicemanInvInCar.State.FindVillager:
                animator.SetBool("Walk", policemanInvInCar.VillagerNearyby);
                break;
            case PolicemanInvInCar.State.Investigate:
                animator.SetBool("Text", true);
                break;
            case PolicemanInvInCar.State.BackToCar:
                animator.SetBool("Walk", !policemanInvInCar.agent.isStopped);
                break;
            default:
                break;
        }
    }

   
}
