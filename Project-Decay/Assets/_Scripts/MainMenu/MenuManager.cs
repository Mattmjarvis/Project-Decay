using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void newGame()
    {
        SceneManager.LoadScene("01");
    }
    public void ContinueGame()
    {
        //Continue game code, load last save etc.
    }
    public void Options()
    {
        //Options menu code
    }
    public void Exit()
    {
        Application.Quit();
    } 

}
