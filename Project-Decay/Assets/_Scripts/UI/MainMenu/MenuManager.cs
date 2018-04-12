using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuManager : MonoBehaviour { 

    public GameObject menuButtons;

    public void Start()
    {
        StartCoroutine(MoveButtons());
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        //Continue game code, load last save etc.
    }
    public void ControlMenu()
    {
        //Options menu code
    }
    public void Exit()
    {
        Application.Quit();
    }

    public IEnumerator MoveButtons()
    {
        for(; ;)
        {

            if(menuButtons.transform.localPosition.x >= 0)
            {
                Debug.Log("menuButtons.transform.localPosition.x = " + menuButtons.transform.localPosition.x);
                yield break;
            }
            else
            {
                menuButtons.transform.localPosition = new Vector3(menuButtons.transform.localPosition.x + 20f, menuButtons.transform.localPosition.y, menuButtons.transform.localPosition.z);

            }

            yield return new WaitForSeconds(0.005f);
        }

        //yield return null;
    }

}
