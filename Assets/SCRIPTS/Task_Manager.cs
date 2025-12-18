using UnityEngine;
using UnityEngine.SceneManagement;

public class Task_Manager : MonoBehaviour
{
    public int remainingToys = 20;
    public int remainingTrash = 20;
    public int remainingDishes = 20;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) //Debug Win Condition
        {
            remainingToys--;
            remainingTrash--;
            remainingDishes--;
        }
        if (remainingToys == 0 && remainingDishes == 0 && remainingTrash == 0) //Faz o jogo terminar
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene("Win");
        }
    }
}
