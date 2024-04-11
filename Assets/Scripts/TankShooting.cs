using UnityEngine;
using UnityEngine.Serialization;

public class TankShooting : MonoBehaviour
{
    public Rigidbody2D BulletPrefab;
    public Transform FirePosition;
    public float BulletSpeed = 10f;
    public float BulletLifetime = 2f;
    public GameObject SmokePrefab;
    public float shootCooldown = 3f;
    public CooldownProgressBar ProgressBar;
    public AudioSource TankShootingAudio;

    private GameObject _smoke;
    private float _lastShootTime;

    private void Update()
    {
        if (Time.time - _lastShootTime >= shootCooldown)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Shoot();
                TankShootingAudio.Play();
                _smoke.transform.position = FirePosition.position;
                _lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        Rigidbody2D bulletInstance = Instantiate(BulletPrefab, FirePosition.position, FirePosition.rotation);
        bulletInstance.velocity = FirePosition.up * BulletSpeed;
        
        ProgressBar.StartCooldown();

        _smoke = Instantiate(SmokePrefab, FirePosition.position, FirePosition.rotation);
        Destroy(_smoke, _smoke.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);


        Destroy(bulletInstance.gameObject, BulletLifetime);
    }
}