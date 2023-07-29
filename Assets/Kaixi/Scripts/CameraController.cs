using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // the target object to follow
    public float height = 10f; // the height of the camera
    public float distance = 20f; // the distance of the camera from the target
    public float damping = 5f; // the smoothness of the camera movement



    void LateUpdate()
    {
        // calculate the target position and rotation of the camera
        Vector3 targetPosition = target.position + Vector3.up * height - target.forward * distance;
        Quaternion targetRotation = Quaternion.LookRotation(target.position - targetPosition, Vector3.up);

        // smoothly move the camera to the target position and rotation
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * damping);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * damping);
    }
}

