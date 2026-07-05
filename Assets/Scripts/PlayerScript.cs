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
    public float moveSpeed = 100f;
    void Start()
    {
        actionMovement.action.Enable();
        actionLook.action.Enable();
        print("Ich bin Søren");
        rb = GetComponent<Rigidbody>();
    }

    void OnDisable()
    {
        actionMovement.action.Disable();
        actionLook.action.Disable();
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
}
