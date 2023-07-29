using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RandomActive : MonoBehaviour
{

    private void Awake()
    {
       //�������������������
        for (int i = 0; i < transform.childCount; i++)
        {
            int randomNum = Random.Range(0, 2);
            transform.GetChild(i).gameObject.SetActive(randomNum > 0);
        }

        //int randomNum = Random.Range(0, 2);
        //gameObject.SetActive(randomNum > 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
