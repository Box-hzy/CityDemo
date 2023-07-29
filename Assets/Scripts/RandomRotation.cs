using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        transform.Rotate(Vector3.up, Random.Range(-180,180), Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
