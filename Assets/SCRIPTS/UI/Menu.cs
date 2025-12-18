using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject button;
    public void update()
    {

    }
    public void MainMenu(string Menu)
    {
        SceneManager.LoadScene(Menu);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayGame(string Game)
    {
        SceneManager.LoadScene(Game);
    }
    public void CreditsButton()
    {
        if (button.gameObject.activeSelf)
        {
            button.SetActive(false);
        }
        else
        {
            button.SetActive(true);
        }
        
    }

}
