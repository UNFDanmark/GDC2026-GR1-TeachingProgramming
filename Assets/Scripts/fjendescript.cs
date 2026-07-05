using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class fjendescript : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject destination;
    public GameObject bulletPrefab;
    public GameObject shootingPoint;
    public float bulletspeed = 5;
    public float seeDistance = 8;
    public float bulletExpire = 2;
    public float Cooldown = 10.0f;
    float CooldownLeft;
    GameObject bullet;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = FindAnyObjectByType<PlayerScript>().gameObject;
    }
    void Update()
    {
        agent.SetDestination(destination.transform.position);
        if (CooldownLeft < 0)
        {
            RaycastHit[] cool = Physics.RaycastAll(shootingPoint.transform.position, transform.forward, seeDistance);
            foreach (RaycastHit cruel in cool)
            {
                if (cruel.collider.gameObject.CompareTag("Player"))
                {
                    bullet = Instantiate(bulletPrefab, shootingPoint.transform.position,
                        shootingPoint.transform.rotation);
                    bullet.GetComponentInChildren<Rigidbody>()
                        .AddForce(transform.forward * bulletspeed, ForceMode.Impulse);
                    GameObject.Destroy(bullet, bulletExpire);
                    CooldownLeft = Cooldown;
                }
            }
        }
        CooldownLeft -= Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet")) Destroy(gameObject);
    }
}
