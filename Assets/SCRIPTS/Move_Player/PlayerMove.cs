using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    public bool Light = false;

    public Animator animator;
    public bool isHidden = false;

    public CharacterController controller;
    public float speed = 1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float sensibilidadeX = 2f;
    float rotY = 0f;
    
    

    Vector3 velocity;


    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 3f;
            animator.speed = 2f;
            
        }
        else
        {
            speed = 1.5f;
            animator.speed = 1f;
            
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        // Gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        rotY += Input.GetAxis("Mouse X") * sensibilidadeX;
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);

        if (z != 0)
        {

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            lanterna();
        }*/
    }


    public void lanterna()
    {
        if (Light == false)
        {
            flashlight.SetActive(true);
            Light = true;
        }
        if (Light == true)
        {
            flashlight.SetActive(false);
            Light = false;
        }
    }
}
