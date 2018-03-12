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
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            NextImage();
        }
    }

    // Changes to the next image in the array
    public void NextImage()
    {
        if (nextImage <= cutsceneImages.Length - 1)
        {
            fader.SceneFadeOutBlack();
            currentImage.GetComponent<SpriteRenderer>().sprite = cutsceneImages[nextImage];
            nextImage++;
        }
        else
        {
            fader.SceneFadeOutBlack();
            Debug.Log("load scene");
            //SceneManager.LoadScene(3);
        }
    }
}
