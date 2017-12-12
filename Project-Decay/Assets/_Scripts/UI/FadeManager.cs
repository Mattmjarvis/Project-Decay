using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour {

    public Image fadeBlackImageIn;
    public GameObject fadeImageIn;
    public Image fadeBlackImageOut;
    public GameObject fadeImageOut;

    Color resetFadeOut;
    Color resetFadeIn;
   
    private void Awake()
    {
        resetFadeOut = new Color(fadeBlackImageOut.color.r, fadeBlackImageOut.color.g, fadeBlackImageOut.color.b, 0.01f);
        resetFadeIn = new Color(fadeBlackImageIn.color.r, fadeBlackImageIn.color.g, fadeBlackImageIn.color.b, 1f);
        fadeBlackImageOut.color = resetFadeOut;

        fadeBlackImageIn.color = resetFadeIn;

        StartCoroutine(SceneBlackFadeIn());
    }

    // Resets fade variables 
    private void OnLevelWasLoaded()
    {

        fadeBlackImageOut.color = resetFadeOut;
        fadeBlackImageIn.color = resetFadeIn;

        StartCoroutine(SceneBlackFadeIn());
    }

    // Fades the scene in to full colour
    public void SceneFadeInBlack()
    {
        StartCoroutine(SceneBlackFadeIn());
    }

    // Fades the scene out to black
    public void SceneFadeOutBlack()
    {
        StartCoroutine(SceneBlackFadeOut());
    }
       
    // Scene fades out to black
    IEnumerator SceneBlackFadeOut()
    {

        fadeImageOut.SetActive(true);

        while (fadeBlackImageOut.color.a < 1f)
        {
            fadeBlackImageOut.color = new Color(fadeBlackImageOut.color.r, fadeBlackImageOut.color.g, fadeBlackImageOut.color.b, fadeBlackImageOut.color.a + (Time.deltaTime / 2f));
            yield return null;
        }

        if (fadeBlackImageOut.color.a >= 1f)
        {
            SceneManager.LoadScene("03_Game");
        }

    }

    // Fades scene in to full colour
    IEnumerator SceneBlackFadeIn()
    {
 
        fadeImageIn.SetActive(true);

        while (fadeBlackImageIn.color.a > 0.01f)
        {
            fadeBlackImageIn.color = new Color(fadeBlackImageIn.color.r, fadeBlackImageIn.color.g, fadeBlackImageIn.color.b, fadeBlackImageIn.color.a - (Time.deltaTime / 3f));
            yield return null;
        }

    }
}