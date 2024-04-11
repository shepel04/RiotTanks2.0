using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDestroyer : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public GameObject ReplacementPrefab;
    public GameObject Canvas;
    public AudioSource DestroyAudio;
    public GameObject Player;

    private GameObject _hud;
    private TankController _playerController;

    private void Start()
    {
        _playerController = Player.GetComponent<TankController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject explosion = Instantiate(ExplosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
            DestroyAudio.Play();
            
            Destroy(gameObject); 
            Instantiate(ReplacementPrefab, other.transform.position, Quaternion.identity);

            _hud = GameObject.Find("HUD");
            
            _hud.SetActive(false);
            Canvas.SetActive(true);
            _playerController.enabled = false;

        }
    }
}
