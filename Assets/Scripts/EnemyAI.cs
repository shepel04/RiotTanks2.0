using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Diagnostics;
using UnityEngine.Serialization;

public class EnemyAI : MonoBehaviour
{
    public Transform BotTarget;  
    public Quaternion CurrentRotation;
    public float ZAngle;
    
    private NavMeshAgent _navMeshAgent;  
    private BotController _bot;
    private GameObject _localTarget;
    

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();  
        
        _bot = GetComponent<BotController>();
        
        _navMeshAgent.updateRotation = false;
    }

    public void MoveTowardsTarget()
    {
        if (_navMeshAgent != null && BotTarget != null)
        {
            _navMeshAgent.SetDestination(BotTarget.transform.position);
        }
    }
    
    private void Update()
    {
        if (BotTarget == null)
        {
            _navMeshAgent.enabled = false;
            transform.rotation = Quaternion.Euler(0f,0f,ZAngle);
            this.enabled = false;
        }
        else
        {
            if (name == "Enemy" || name == "Enemy(Clone)")
            {
                _localTarget = GameObject.Find("Player");
                BotTarget = _localTarget.transform;
            }
            else
            {
                _localTarget = GameObject.Find("FlagUkraine");
                BotTarget = _localTarget.transform;
            }
            
            MoveTowardsTarget();
            Vector3 direction = (BotTarget.position - transform.position).normalized;
        
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            ZAngle = angle;
        }
    }
}