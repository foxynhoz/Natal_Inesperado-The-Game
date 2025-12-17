using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
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

}
