using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TurretRotation : MonoBehaviourPun
{
    public float speed = 1000f;
    public AudioSource TurretRotationSound;
    
    private float _rotationAmount;
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (!TurretRotationSound.isPlaying)  
                TurretRotationSound.Play();
            
            _rotationAmount = speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * _rotationAmount);
        }
        else if (Input.GetKey(KeyCode.X))
        {
            if (!TurretRotationSound.isPlaying)  
                TurretRotationSound.Play();
            
            _rotationAmount = -speed * Time.deltaTime;
            transform.Rotate(Vector3.forward * _rotationAmount);
        }
        else
        {
            TurretRotationSound.Stop();
            
        }
    }
}
