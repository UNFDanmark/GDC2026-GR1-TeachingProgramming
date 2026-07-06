using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    GameObject bullet;
    AudioSource audiosource;
    [Header("Movement")]
    public InputActionReference actionMovement;
    public float moveSpeed = 100f;
    [Header("Look")]
    public InputActionReference actionLook;
    public GameObject cameraGameObject;
    public float cameraSensX;
    public float cameraSensY;
    [Header("Restart")]
    public InputActionReference actionRestart;
    public Canvas gameOverCanvas;
    [Header("Health")]
    public TMP_Text healthTMPText;
    public int maxHealth = 3;
    int health;
    [Header("Shoot")]
    public InputActionReference actionShoot;
    public GameObject bulletPrefab;
    public GameObject shootingPoint;
    public float bulletspeed = 5;
    public float bulletExpire = 2.0f;
    public float Cooldown = 10.0f;
    float CooldownLeft;
    void Start()
    {
        actionMovement.action.Enable();
        actionLook.action.Enable();
        actionShoot.action.Enable();
        actionRestart.action.Enable();
        actionShoot.action.started += Shoot;
        actionRestart.action.started += Restart;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        audiosource = GetComponentInChildren<AudioSource>();
        health = maxHealth;
        healthTMPText.text = "Health: " + health + " / " + maxHealth;
    }

    void OnDisable()
    {
        actionMovement.action.Disable();
        actionLook.action.Disable();
        actionShoot.action.Disable();
        actionRestart.action.Disable();
        actionShoot.action.started -= Shoot;
        actionRestart.action.started -= Restart;
    }

    void Update()
    {
        Vector2 rawLookInput = actionLook.action.ReadValue<Vector2>();
        Vector3 look = new Vector3(rawLookInput.y * cameraSensY, 0, 0);
        cameraGameObject.transform.Rotate(look);
        rb.transform.Rotate(Vector3.up, rawLookInput.x * cameraSensX);

        Vector2 rawMoveInput = actionMovement.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(rawMoveInput.x, 0, rawMoveInput.y);
        move *= moveSpeed * rb.linearDamping * Time.deltaTime;
        rb.AddRelativeForce(move);

        animator.SetFloat("speed", rb.linearVelocity.magnitude);
        CooldownLeft -= Time.deltaTime;
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
            Destroy(collision.gameObject);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            healthTMPText.text = "Health: " + health + " / " + maxHealth;
            if (health <= 0)
            {
                gameOverCanvas.gameObject.SetActive(true);
            }
        }
    }

    void Shoot(InputAction.CallbackContext context)
    {
        if (CooldownLeft <= 0)
        {
            bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            Rigidbody rbbullet = bullet.GetComponentInChildren<Rigidbody>();
            rbbullet.linearVelocity = rb.linearVelocity;
            rbbullet.AddForce(transform.forward * bulletspeed, ForceMode.Impulse);
            GameObject.Destroy(bullet, bulletExpire);
            animator.SetTrigger("shoot");
            audiosource.Play();
            CooldownLeft = Cooldown;
        }
    }

    void Restart(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
