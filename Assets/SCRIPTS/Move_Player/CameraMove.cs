using UnityEngine;

public class CameraMove: MonoBehaviour
{

    public float sensibilidadeY = 2f;
    public float minAngulo = -80f, maxAngulo = 80f;
    float rotX = 0f;

    bool travarMouse = true;

    void Start()
    {
        if (!travarMouse)
            return;
        Cursor.visible = false; //Oculta o cursor do mouse
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor no centro
    }
    void Update()
    {
        rotX -= Input.GetAxis("Mouse Y") * sensibilidadeY;
        rotX = Mathf.Clamp(rotX, minAngulo, maxAngulo);
        transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
    }

}
