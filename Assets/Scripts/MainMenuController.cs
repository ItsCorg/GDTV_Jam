using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void StartLevel()
    {
        SceneManager.LoadScene("Sofiane-gym"); 
    } 

    public void QuitGame()
    {
        Application.Quit(); 
    }
}