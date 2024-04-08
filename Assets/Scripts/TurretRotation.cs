using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TurretRotation : MonoBehaviourPun
{
    public float speed = 1000f;
    private float rotationAmount;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            rotationAmount = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
        }
        if (Input.GetKey(KeyCode.X))
        {
            rotationAmount = -speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * rotationAmount);
        }
    }
}
