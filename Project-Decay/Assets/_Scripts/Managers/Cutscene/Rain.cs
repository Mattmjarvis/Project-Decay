using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour {

    // Rain drop variables
    public Image[] raindrops;
    private int newDrop;
    private int oldDrop;
     
    // Start!
    public void Start()
    {
        StartCoroutine(Raindrops());
    }

    // Update is called once per frame
    void FixedUpdate () {	

        // Reduce the alpha of any active images to get a fadeout effect
        foreach(Image image in raindrops)
        {
            if (image.color.a > 0f)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - (Time.deltaTime / 3f)); // Fadeout the image
            }            
        }
	}
    
    // Generates random number for the rain images
    public void RandomNumber()
    {
        newDrop = Random.Range(0, raindrops.Length); // Generate random number

        // Apply the new drop 
        if (newDrop != oldDrop)
        {
            oldDrop = newDrop;
        }

        // Reroll for a new number if it is equal to last
        else
        {
            RandomNumber();
        }
    }

    // Loops random rain images to appear
    IEnumerator Raindrops()
    {
        RandomNumber(); // Get new raindrop
        yield return new WaitForSeconds(Random.Range(1.5f, 5f)); // Wait random amount of time
        raindrops[oldDrop].color = Color.white; // Reset colour to full
        StartCoroutine(Raindrops()); // Repeat
    }
}
