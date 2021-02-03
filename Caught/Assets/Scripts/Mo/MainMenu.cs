using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       // SceneManager.LoadScene(1, LoadSceneMode.Single);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();

    }
}
