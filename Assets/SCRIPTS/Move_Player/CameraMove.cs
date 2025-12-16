using UnityEngine;

public class CameraMove: MonoBehaviour
{
    [SerializeField] GameObject Player;
    public bool travarMouse = true; //Controla se o cursor do mouse é exibido
    public float sensibilidade = 2.0f; //Controla a sensibilidade do mouse
    public float minAnguloY = -90f;  // Limite inferior (ex: não olhar abaixo)
    public float maxAnguloY = 90f;   // Limite superior (ex: não olhar acima)
    private float mouseX = 0.0f, mouseY = 0.0f; //Controla a rotação do mouse

    void Start()
    {
        if (!travarMouse)
            return;


        Cursor.visible = false; //Oculta o cursor do mouse
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor no centro


    }
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensibilidade; // Incrementa o valor do eixo X
        mouseY -= Input.GetAxis("Mouse Y") * sensibilidade; // Incrementa o valor do eixo Y (inverte para naturalidade)
        mouseY = Mathf.Clamp(mouseY, minAnguloY, maxAnguloY);  // Trava o ângulo Y
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0); //Aplica a rotação
        transform.position = Player.transform.position; //Mantém a câmera na posição do jogador

    }
}
