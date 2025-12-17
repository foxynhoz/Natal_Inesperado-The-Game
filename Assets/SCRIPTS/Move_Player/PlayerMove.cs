using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject flashlight;
    public bool Light = false;

    public Animator animator;
    

    public CharacterController controller;
    public float speed = 1f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float sensibilidadeX = 2f;
    float rotY = 0f;
    Camera cam;
    Interaction_Script Interaction_Script;
    

    Vector3 velocity;
    private void Start()
    {
        cam = Camera.main;
        Interaction_Script = Camera.main.GetComponent<Interaction_Script>();
    }

    void Update()
    {
        camMove();

        if(!Interaction_Script.isHidden)
        {
            MoveF();
        }
            
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            lanterna();
        }*/
    }
    void MoveF()
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

        AnimationControl(z);
    }

    void camMove()
    {
        rotY += Input.GetAxis("Mouse X") * sensibilidadeX;
        transform.rotation = Quaternion.Euler(0f, rotY, 0f);
    }
    void AnimationControl(float z)
    {
        if (z != 0)
        {

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
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
