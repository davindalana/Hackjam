using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Vector3 newgame = new Vector3(-15.29543f, -1.467768f, 0.06722727f);

    public void NewGame()
    {
        GameManager.SaveGame(newgame);
        SceneManager.LoadScene("Demo");
    }
    public void LoadGame()
    {
        SceneManager.LoadScene("Demo");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
