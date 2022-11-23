using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAT; // Look at Obi.
    public Vector3 offset = new Vector3 (0, 5.0f, -10.0f);

    private void Start() 
    {
        transform.position = lookAT.position + offset;
    }

    private void LateUpdate() 
    {
        Vector3 desiredPosition = lookAT.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime);
    }
}
