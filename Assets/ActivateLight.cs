using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateLight : MonoBehaviour
{
    public void Activate()
    {
        GameManager.Instance.activateLight = true;
        Debug.Log("ONN");
    }

    public void Deactivate()
    {
        GameManager.Instance.activateLight = false;
        Debug.Log("OFF");
    }

    public void Test()
    {
        Debug.Log("testt");
    }
}
