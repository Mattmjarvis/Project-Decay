﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCutsceneController : MonoBehaviour
{

    // Audio
    public AudioSource player;
    public AudioClip lightning;


    // Get all images and game objects
    public Sprite[] cutsceneImages;
    public GameObject currentImage;
    public GameObject continueText;
    public int nextImage = 1; // Set the next image

    public bool changing = false; // Bool to check if the image is already changing

    // Components
    FadeManager fader; // Fade manager component
    TextController textContol;

    // Find the fade manager and alert it to cutscene
    public void Awake()
    {
        textContol = FindObjectOfType<TextController>();
        fader = FindObjectOfType<FadeManager>();
        fader.cutscene = true;
    }

    public void Update()
    {
        // Moves to next point in cutscene if the spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space) && changing == false)
        {
            textContol.StopTyping();
            changing = true; // Alert to change
            NextImage(); // Begin changing image coroutine
        }
    }

    // Changes to the next image in the array
    public void NextImage()
    {
        StartCoroutine(NextImageInCutscene());
        textContol.StartTyping();
    }

    // Numerator changes image if there are more available, fades in/out each image
    IEnumerator NextImageInCutscene()
    {
        // Checks for more images
        if (nextImage <= cutsceneImages.Length - 1)
        {
            continueText.SetActive(false); // Turns text off
            fader.SceneFadeOutBlack(); // Begin fadeout


            yield return new WaitForSeconds(0.8f);
            // After 0.8s (black screen) the image will change and fade scene back in
            currentImage.GetComponent<Image>().sprite = cutsceneImages[nextImage];


            fader.SceneFadeInBlack(); // Begin fade in
            nextImage++; // Increments image counter

            yield return new WaitForSeconds(2f);
            // After 2 seconds set changing to false and show text
            continueText.SetActive(true);
            changing = false;
        }

        // If the final image has been reached then we fade out and change to game scene
        else
        {
            fader.cutscene = false;
            fader.SceneFadeOutBlack();
            continueText.SetActive(false);
          

            yield return new WaitForSeconds(5f);
            EndCutscene();
        }

    }



    // Ends the game
    public void EndCutscene()
    {
        SceneManager.LoadScene(4);
    }
}
