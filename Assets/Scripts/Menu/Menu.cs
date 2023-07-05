using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void ChangeScreen(string screenName)
    {
        SceneManager.LoadScene(screenName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
