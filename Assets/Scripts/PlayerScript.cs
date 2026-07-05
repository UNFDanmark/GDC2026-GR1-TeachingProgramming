using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Transform transform;
    public InputActionReference actionMovement;
    public InputActionReference actionLook;
    public float cameraSensX;
    public float cameraSensY;
    public GameObject cameraGameObject;
    Rigidbody rb;
    Animator animator;
    public float moveSpeed = 100f;
    public GameObject bulletPrefab;
    public GameObject shootingPoint;
    public float bulletspeed = 5;
    GameObject bullet;
    public InputActionReference actionShoot;
    public float bulletExpire = 2.0f;
    public float Cooldown = 10.0f;
    float CooldownLeft;
    void Start()
    {
        actionMovement.action.Enable();
        actionLook.action.Enable();
        actionShoot.action.Enable();
        actionShoot.action.started += Shoot;
        print("Ich bin Søren");
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnDisable()
    {
        actionMovement.action.Disable();
        actionLook.action.Disable();
        actionShoot.action.Disable();
        actionShoot.action.started -= Shoot;
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
            print("Du tabte");
        }
    }

    void Shoot(InputAction.CallbackContext context)
    {
        if (CooldownLeft <= 0)
        {
            bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            bullet.GetComponentInChildren<Rigidbody>().AddForce(transform.forward * bulletspeed, ForceMode.Impulse);
            GameObject.Destroy(bullet, bulletExpire);
            animator.SetTrigger("shoot");
            CooldownLeft = Cooldown;
        }
    }
}
