using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(CreditEnd());
    }

    IEnumerator CreditEnd()
    {

        yield return new WaitForSeconds(35f);
        SceneManager.LoadScene(0);


    }
}

