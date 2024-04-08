using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBlock : MonoBehaviour
{
    public GameObject EnemyType1; 
    public GameObject EnemyType2;

    private Vector3 _leftOffset;
    private Vector3 _rightOffset;

    private void Start()
    {
        _leftOffset = new Vector3(-3, -1);
        _rightOffset = new Vector3(3, -1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            BreakBlock();
        }
    }

    public void BreakBlock()
    {
        Vector3 blockPosition = transform.position;

        Instantiate(EnemyType1, blockPosition + _leftOffset, Quaternion.identity);
        
        Instantiate(EnemyType2, blockPosition + _rightOffset, Quaternion.identity);

        Destroy(gameObject);
    }
}