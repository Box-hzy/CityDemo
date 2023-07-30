using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class RandomActive : MonoBehaviour
{
    //set up 
    [Range(0,100)]
    public int randomProbability = 50;

    private void Awake()
    {
       //随机允许多个场景物体在
        for (int i = 0; i < transform.childCount; i++)
        {
            int randomNum = Random.Range(0, 101);
            transform.GetChild(i).gameObject.SetActive(randomNum >= randomProbability);
            Debug.Log(randomNum >= randomProbability);

            //use box collider instead of mesh collider (重复的代码，后期可以另建一个公共函数脚本)
            if (randomNum < randomProbability) return;
            Transform target = transform.GetChild(i);
            target.TryGetComponent<MeshCollider>(out MeshCollider mesh);
            if(mesh) mesh.enabled = false;
            target.AddComponent<BoxCollider>();
        }

        
    }



}
