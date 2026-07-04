using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Transform transform;
    Rigidbody rb;
    public InputAction RotAction;
    public float speed = 100f;
    public float cooldown = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RotAction.Enable();
        print("Ich bin Søren");
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 cool = RotAction.ReadValue<Vector2>();
        if (cool.x > 0) transform.Rotate(0, 2, 0);
        else if (cool.x < 0) transform.Rotate(0,-2,0);
        rb.AddRelativeForce(new Vector3(0, 0, cool.y * speed * rb.linearDamping));
    }
}
