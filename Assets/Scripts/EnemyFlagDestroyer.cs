using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyFlagDestroyer : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public GameObject ReplacementPrefab;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyFlag") || other.CompareTag("PlayerFlag"))
        {
            GameObject explosion = Instantiate(ExplosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
            Destroy(other.gameObject); 
            GameObject dust = Instantiate(ReplacementPrefab, other.transform.position, Quaternion.identity);
            
        }
    }
}
