using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletExplosion : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collision");
        GameObject explosion = Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }
}