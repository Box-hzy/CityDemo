using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<PickUpStick>(out PickUpStick pickUpStick);
            pickUpStick.SetPickUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.TryGetComponent<PickUpStick>(out PickUpStick pickUpStick);
            pickUpStick.SetPickUp();
        }
    }
}
