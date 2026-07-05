using UnityEngine;

public class CursorScript : MonoBehaviour
{
    static CursorScript instance;
    void Start()
    {
        if(instance != null) Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
