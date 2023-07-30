using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour
{
    [SerializeField]private Animation anim;
    bool isOn;
    [SerializeField] private Light lamp;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        lamp = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.activateLight == true)
        {
            if (isOn) return;
            Debug.Log("LightOn");
            anim.Play("StreetLampOn");
            isOn = true;
        }
        else
        {
            if (!isOn) return;
            Debug.Log("LightOff");
            anim.Play("StreetLampOff");
        }

        //for testing
        //if (Input.GetKey(KeyCode.K)) { anim.Play("StreetLampOff"); }
    }

    //public void lightOnState()
    //{
    //    isOn = true;
    //}

    public void lightOffState()
    {
        isOn = false;
    }
}
