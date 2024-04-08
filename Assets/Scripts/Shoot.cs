using UnityEngine;
using UnityEngine.Serialization;

public class Shoot : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform FirePoint;
    public float ShootInterval = 4f; 
    
    private float _shootTimer = 0f;
    private Transform _target;

    void Update()
    {
        if (_target != null && Time.time >= _shootTimer)
        {
            ShootBullet();
            _shootTimer = Time.time + ShootInterval;
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab, FirePoint.position, FirePoint.rotation);
    }

    public void SetTarget(Transform newTarget)
    {
        _target = newTarget;
    }
}