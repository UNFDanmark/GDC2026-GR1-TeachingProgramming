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
    //public InputAction RotAction;
    public float moveSpeed = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionMovement.action.Enable();
        actionLook.action.Enable();
        //RotAction.Enable();
        print("Ich bin Søren");
        rb = GetComponent<Rigidbody>();
        //transform = GetComponent<Transform>();
    }

    void OnDisable()
    {
        actionMovement.action.Disable();
        actionLook.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 cool = RotAction.ReadValue<Vector2>();
        //if (cool.x > 0) transform.Rotate(0, 2, 0);
        //else if (cool.x < 0) transform.Rotate(0,-2,0);
        //rb.AddRelativeForce(new Vector3(0, 0, cool.y * speed * rb.linearDamping));
        
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
}
