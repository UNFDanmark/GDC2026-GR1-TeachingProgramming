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
    public InputActionReference shoot;


    public float bulletExpire = 2.0f;
    public float Cooldown = 10.0f;
    float CooldownLeft;
    
    void Start()
    {
        shoot.action.Enable();
    }

    void Update()
    {
        if (shoot.action.IsPressed() && CooldownLeft <= 0)
        {
            bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * bulletspeed, ForceMode.Impulse);
            GameObject.Destroy(bullet,bulletExpire);
            CooldownLeft = Cooldown;
        }
        CooldownLeft -= Time.deltaTime;
    }
}
