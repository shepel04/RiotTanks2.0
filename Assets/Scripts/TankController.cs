using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TankController : MonoBehaviourPun
{
    public float HealthPoints = 100f;
    public float MovementSpeed = 5f;
    public float RotationSpeed = 100f;
    public GameObject TankExplosionPrefab;
    public GameObject ReplacementTankPrefab;
    public float TankDestroyDelay = 3f;
    public bool IsArmorActive;
    public GameObject TankSprite;
    public GameObject LoseCanvas;
    public AudioSource DriveAudio, StayAudio, DestroyAudio;
    
    private Rigidbody2D _tankRigidbody;
    private bool _isMoving;
    private Animator _tankAnimator;
    private GameObject _hud;
    
    void Start()
    {
        _tankRigidbody = GetComponent<Rigidbody2D>();
        _tankAnimator = TankSprite.GetComponent<Animator>();
    }
    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float rotateInput = Input.GetAxis("Horizontal");

        // movement
        Vector3 movement = transform.up * moveInput * MovementSpeed * Time.deltaTime;
        transform.position += movement;

        // rotation
        float rotationAmount = -rotateInput * RotationSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationAmount);
        
        _isMoving = Mathf.Abs(moveInput) > 0.1f;
        if (_isMoving)
        {
            if (StayAudio.isPlaying) 
                StayAudio.Stop();
            if (!DriveAudio.isPlaying) 
                DriveAudio.Play();
        }
        else
        {
            if (DriveAudio.isPlaying) 
                DriveAudio.Stop();
            if (!StayAudio.isPlaying) 
                StayAudio.Play();
        }
        _tankAnimator.SetBool("IsMoving", _isMoving);
        
        if (HealthPoints <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        GameObject explosion = Instantiate(TankExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        DestroyAudio.Play();
        
        Destroy(gameObject); 
        Instantiate(ReplacementTankPrefab, transform.position, Quaternion.identity);
        
        _hud = GameObject.Find("HUD");
            
        _hud.SetActive(false);
        LoseCanvas.SetActive(true);
        enabled = false;
    }
}
