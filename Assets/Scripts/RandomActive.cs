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
       //�������������������
        for (int i = 0; i < transform.childCount; i++)
        {
            int randomNum = Random.Range(0, 101);
            transform.GetChild(i).gameObject.SetActive(randomNum >= randomProbability);
            Debug.Log(randomNum >= randomProbability);

            //use box collider instead of mesh collider (�ظ��Ĵ��룬���ڿ�����һ�����������ű�)
            if (randomNum < randomProbability) return;
            Transform target = transform.GetChild(i);
            target.TryGetComponent<MeshCollider>(out MeshCollider mesh);
            if(mesh) mesh.enabled = false;
            target.AddComponent<BoxCollider>();
        }

        
    }



}
