//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PatrolPoliceAnimation : MonoBehaviour
//{
//    PolicemanPatrolInCar policemanPatrolInCar;
//    Animator animator;
//    PolicemanPatrolInCar.State currentState;
//    private void Start()
//    {
//        animator = GetComponent<Animator>();
//        policemanPatrolInCar = GetComponent<PolicemanPatrolInCar>();
//        currentState = policemanPatrolInCar.getState();
//    }

//    private void Update()
//    {
//        StateAnimation();
//    }

//    void StateAnimation()
//    {
//        currentState = policemanPatrolInCar.getState();

//        switch (currentState)
//        {
//            case PolicemanPatrolInCar.State.Patrol:
//                animator.SetBool("Walk", !policemanPatrolInCar.agent.isStopped);
//                animator.SetBool("Run", false);
//                break;
//            case PolicemanPatrolInCar.State.Chase:
//                animator.SetBool("Run", true);
//                animator.SetBool("Walk", false);
//                break;
//            case PolicemanPatrolInCar.State.BackToCar:
//                animator.SetBool("Walk", !policemanPatrolInCar.agent.isStopped);
//                animator.SetBool("Run", false);
//                break;
//            default:
//                break;
//        }
//    }

   
//}
