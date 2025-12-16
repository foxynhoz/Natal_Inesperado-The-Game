using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float sensibilidadeX = 2f;
    float rotY = 0f;

    Vector3 velocity;


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        rotY += Input.GetAxis("Mouse X") * sensibilidadeX;
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
    }
}
