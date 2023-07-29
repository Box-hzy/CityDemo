using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RamdonModel : MonoBehaviour
{
    //public List<GameObject> Buildings;
   
    private void Awake()
    {
        int randomNum = Random.Range(0, transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            //Ëæ»úÑÕÉ«
            //Buildings.Add(transform.GetChild(i).gameObject);
            transform.GetChild(i).gameObject.SetActive(i == randomNum);
        }

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
