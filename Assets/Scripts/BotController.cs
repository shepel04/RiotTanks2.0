using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class BotController : MonoBehaviour
{
    public float RotationSpeed = 3f;
    public float DetectionRadius = 10f;
    public float ShootCooldown = 4f;
    public float BulletSpeed = 10f;
    public float BulletLifetime = 2f;
    public Rigidbody2D BulletPrefab;
    public GameObject SmokePrefab;
    public Transform FirePoint;
    public Transform BotTarget;
    
    private NavMeshAgent navMeshAgent;
    private Transform _shootScript;
    private GameObject _smoke;
    private float _lastShootTime;
    private EnemyAI _ai;
    private Quaternion _previousRotation;
    private AudioSource _shootAudio;

    
    private void Start()
    {
        _lastShootTime = Time.time;
        _ai = GetComponent<EnemyAI>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        GameObject obj = GameObject.Find("ShootSound");
        _shootAudio = obj.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (BotTarget == null)
        {
            FindTarget();
            _ai = GetComponent<EnemyAI>();
            _ai.MoveTowardsTarget();
            _previousRotation = transform.rotation;
            if (_ai.BotTarget != null)
            {
                navMeshAgent.enabled = true;
                _ai.enabled = true;
            }
            
        }
        else
        {
            if (navMeshAgent.isActiveAndEnabled)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, _ai.ZAngle);
                
            }
            navMeshAgent.enabled = false;
            _ai.enabled = false;
            
            RotateTowardsPlayer();
            Shoot();
            FindTarget();        
            
        }
    }

    void FindTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, DetectionRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("PlayerFlag"))
            {
                BotTarget = collider.transform;
                return;
            }
        }
        BotTarget = null;
    }

    void RotateTowardsPlayer()
    {
        if (BotTarget != null)
        {
            Vector3 direction = BotTarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, RotationSpeed * Time.deltaTime * 0.5f);
        }
        else
        {
            // Smoothly interpolate towards the previous rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _previousRotation, RotationSpeed * Time.deltaTime * 0.5f);
        }
    }

    void Shoot()
    {
        if (Time.time - _lastShootTime >= ShootCooldown)
        {
            Rigidbody2D bulletInstance = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
            bulletInstance.velocity = FirePoint.up * BulletSpeed;
            _shootAudio.Play();
            _smoke = Instantiate(SmokePrefab, FirePoint.position, FirePoint.rotation);
            Destroy(_smoke, _smoke.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

            Destroy(bulletInstance.gameObject, BulletLifetime);

            _lastShootTime = Time.time;
        }
    }
}