using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class RandomModel : MonoBehaviour
{
    //public List<GameObject> Buildings;

    private void Awake()
    {
        int randomNum = Random.Range(0, transform.childCount);
        for (int i = 0; i < transform.childCount; i++)
        {
            //random model,choose one of them to be actived
            transform.GetChild(i).gameObject.SetActive(i == randomNum);

            //use box collider instead of mesh collider
            if (i != randomNum) continue;
            if (transform.CompareTag("NPC")) continue;

            Transform target = transform.GetChild(i);
            target.TryGetComponent<MeshCollider>(out MeshCollider mesh);
            if (mesh) mesh.enabled = false;
            target.AddComponent<BoxCollider>();
        }

    }

}
