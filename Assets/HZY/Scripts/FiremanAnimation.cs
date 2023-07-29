using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanAnimation : MonoBehaviour
{
    Animator animator;
    FiremanScript firemanScript;
    public GameObject water;
    bool agentIsStopped;
    private void Start()
    {
        animator = GetComponent<Animator>();
        firemanScript = GetComponent<FiremanScript>();
        water.SetActive(false);
       
    }

    private void Update()
    {
        //if (firemanScript.FirefighterAgent.isStopped)
        //{
        //    water.SetActive(true);
        //    animator.SetBool("Run", false);
        //    animator.SetBool("PutingOff", true);
        //}
        //else
        //{

        //}

        agentIsStopped = firemanScript.FirefighterAgent.isStopped;

        water.SetActive(agentIsStopped);
        animator.SetBool("Run", !agentIsStopped);
        animator.SetBool("PutingOff", agentIsStopped);

    }
}
