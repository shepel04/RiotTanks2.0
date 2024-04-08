using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagDestroyer : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public GameObject ReplacementPrefab;
    public GameObject Canvas;

    private GameObject _hud;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            GameObject explosion = Instantiate(ExplosionPrefab, other.transform.position, Quaternion.identity);
            Destroy(explosion, explosion.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            
            Destroy(gameObject); 
            Instantiate(ReplacementPrefab, other.transform.position, Quaternion.identity);

            _hud = GameObject.Find("HUD");
            
            _hud.SetActive(false);
            Canvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
