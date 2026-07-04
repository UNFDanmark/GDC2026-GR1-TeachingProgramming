using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class shootingscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public GameObject bulletPrefab;
    public GameObject shootingPoint;
    public float bulletspeed = 1000;
    GameObject bullet;
    public InputAction shoot;


    public float bulletExpire = 2.0f;
    public float Cooldown = 10.0f;
    float CooldownLeft;
    
    void Start()
    {
        shoot.Enable();
    }

    // Update is called once per frame
    void Update()
    {



        if (shoot.WasPerformedThisFrame() && CooldownLeft <= 0)
        {
            bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletspeed);
            GameObject.Destroy(bullet,bulletExpire);
            CooldownLeft = Cooldown;
        }

        CooldownLeft -= Time.deltaTime;
        
        //if (CooldownLeft <= 0 && bullet != null) GameObject.Destroy(bullet);
    }
}
