using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Script : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    CameraMove Cam;
    PlayerMove PlayerMove;

    public void callMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void quitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        Cam = Camera.main.GetComponent<CameraMove>();
        PlayerMove = GameObject.Find("Player").GetComponent<PlayerMove>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PauseFunc();
        }
    }

    public void PauseFunc()
    {
        if (PauseMenu.activeSelf)
        {
            Cam.enabled = true;
            PlayerMove.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1f;

        }
        else
        {
            Cam.enabled = false;
            PlayerMove.enabled = false;
            PauseMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;

        }
    }
}
