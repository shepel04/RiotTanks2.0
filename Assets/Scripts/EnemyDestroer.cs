using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyDestroer : MonoBehaviour
{
    public GameObject EnemyExplosionPrefab;
    public GameObject ReplacementEnemyPrefab;
    public GameObject BigEnemyExplosionPrefab;
    public GameObject ReplacementBigEnemyPrefab;
    public float TankDestroyDelay = 3;
    
    private BigEnemy _bigEnemyInst;
    private GameObject _destroyedEnemyCounter;
    private DestroyedEnemyCounter _counter;
    private AudioSource _destroyAudio;
    
    private void Start()
    {
        _destroyedEnemyCounter = GameObject.Find("DestroyedEnemyCounter");
        _counter = _destroyedEnemyCounter.GetComponent<DestroyedEnemyCounter>();
        Debug.Log(PlayerPrefs.GetInt("MaxDestroyedTanks", 0));
        GameObject obj = GameObject.Find("TankDestroySound");
        _destroyAudio = obj.GetComponent<AudioSource>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyEasy"))
        {
            _counter.Enemy += 1;
            BlowUpEnemy(other);
            _destroyAudio.Play();
        }
    
        if (other.gameObject.CompareTag("BigEnemy"))
        {
            if (CompareTag("Mine"))
            {
                BlowUpBigEnemy(other);
                _destroyAudio.Play();
                _counter.BigEnemy += 1;
            }
            else
            {
                _bigEnemyInst = other.gameObject.GetComponent<BigEnemy>();
                _bigEnemyInst.HealthPoints -= 1;
                if (_bigEnemyInst.HealthPoints <= 0)
                {
                    _counter.BigEnemy += 1;
                    BlowUpBigEnemy(other);
                    _destroyAudio.Play();
                }
            }
        }
        
        CheckMaxDestroyed();
    }

    public void CheckMaxDestroyed()
    {
        Debug.Log("Enter");
        int sum = _counter.Enemy / 2 + _counter.BigEnemy / 2;
        if (sum > PlayerPrefs.GetInt("MaxDestroyedTanks", 0)) {
            PlayerPrefs.SetInt("MaxDestroyedTanks", sum);
        }
        
        PlayerPrefs.Save();
    }

    private void BlowUpEnemy(Collision2D other)
    {
        GameObject explosion = Instantiate(EnemyExplosionPrefab, other.transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
        Destroy(other.gameObject); 
        GameObject destroedEnemy = Instantiate(ReplacementEnemyPrefab, other.transform.position, Quaternion.identity);
            
        Destroy(destroedEnemy, TankDestroyDelay);
        FindObjectOfType<EnemyTankSpawner>().EnemyDestroyed();
        
        Destroy(gameObject);
    }
    
    private void BlowUpEnemy(Collider2D other)
    {
        GameObject explosion = Instantiate(EnemyExplosionPrefab, other.transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
        Destroy(other.gameObject); 
        GameObject destroedEnemy = Instantiate(ReplacementEnemyPrefab, other.transform.position, Quaternion.identity);
            
        Destroy(destroedEnemy, TankDestroyDelay);
        FindObjectOfType<EnemyTankSpawner>().EnemyDestroyed();
        
        Destroy(gameObject);
    }
    private void BlowUpBigEnemy(Collider2D other)
    {
        GameObject explosion = Instantiate(BigEnemyExplosionPrefab, other.transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
        Destroy(other.gameObject); 
        GameObject destroedEnemy = Instantiate(ReplacementBigEnemyPrefab, other.transform.position, Quaternion.identity);
            
        Destroy(destroedEnemy, TankDestroyDelay);
        FindObjectOfType<EnemyTankSpawner>().EnemyDestroyed();
        
        Destroy(gameObject);
    }
    
    private void BlowUpBigEnemy(Collision2D other)
    {
        GameObject explosion = Instantiate(BigEnemyExplosionPrefab, other.transform.position, Quaternion.identity);
        Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
        Destroy(other.gameObject); 
        GameObject destroedEnemy = Instantiate(ReplacementBigEnemyPrefab, other.transform.position, Quaternion.identity);
            
        Destroy(destroedEnemy, TankDestroyDelay);
        FindObjectOfType<EnemyTankSpawner>().EnemyDestroyed();
        
        Destroy(gameObject);
    }
    
}
