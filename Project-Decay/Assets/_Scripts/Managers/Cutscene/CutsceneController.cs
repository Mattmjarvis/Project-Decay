using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour {

    public Sprite[] cutsceneImages;
    public GameObject currentImage;
    public int nextImage = 1;

    FadeManager fader;

    public void Awake()
    {   
        fader = FindObjectOfType<FadeManager>();
        fader.cutscene = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NextImage();
        }
    }

    // Changes to the next image in the array
    public void NextImage()
    {
        StartCoroutine(NextImageInCutscene());
    }

        IEnumerator NextImageInCutscene()
        {
        if (nextImage <= cutsceneImages.Length - 1)
        {
            fader.SceneFadeOutBlack();

            yield return new WaitForSeconds(0.8f);
            currentImage.GetComponent<SpriteRenderer>().sprite = cutsceneImages[nextImage];

            Debug.Log("Fade in");
            fader.SceneFadeInBlack();
            nextImage++;
        }
        else
        {
            fader.cutscene = false;
            fader.SceneFadeOutBlack();

            Debug.Log("load scene");
            //SceneManager.LoadScene(3);
        }
    
    }
}
