using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void StartLevel()
    {
        SceneManager.LoadScene("test scene"); 
    } 

    public void QuitGame()
    {
        Application.Quit(); 
    }
}