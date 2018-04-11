using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFadeOut : MonoBehaviour
{
    public MenuManager menuManager;
    public Image fadeBlackImageOut;

    // Fades the scene out to black
    public void SceneFadeOutBlack()
    {
        StartCoroutine(SceneBlackFadeOut());
    }

    // Scene fades out to black
    IEnumerator SceneBlackFadeOut()
    {
        fadeBlackImageOut.gameObject.SetActive(true);

        while (fadeBlackImageOut.color.a < 1f)
        {
            fadeBlackImageOut.color = new Color(fadeBlackImageOut.color.r, fadeBlackImageOut.color.g, fadeBlackImageOut.color.b, fadeBlackImageOut.color.a + (Time.deltaTime / 2f));
            yield return null;
        }

        if (fadeBlackImageOut.color.a >= 1f)
        {
            StopCoroutine(SceneBlackFadeOut());
            menuManager.newGame();
        }

    }
}