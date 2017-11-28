using UnityEngine;
using System.Collections;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int EndLine;

    public TextBoxManager theTextBox;

    public GameObject worldCanvas;
    //public GameObject interactText;

    public bool requireButtonPress;
    private bool waitForPress;

    public bool destroyWhenActivated;

	// Use this for initialization
	void Start ()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();

        //if require button press is true            
        if (requireButtonPress)
        {
            //enable the worldCanvas
            //Debug.Log("Press E to interact");
            EnableWorldCanvas();
            waitForPress = true;
            return;
        }
        else
        {
            //Run SetText.
            SetText();
        }
        //worldCanvas = FindObjectOfType<GameObject>();
        //interactText = FindObjectOfType<GameObject>();
        //worldCanvas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(waitForPress && Input.GetKeyDown(KeyCode.E))
        {
            //if wait for press is true and E has been pressed the "SetText" and disable the world canvas.
            SetText();
            DisableWorldCanvas();
        }
	}

    void SetText()
    {
        theTextBox.ReloadScript(theText);
        theTextBox.currentLine = startLine;
        theTextBox.endAtLine = EndLine;
        theTextBox.EnableTextBox();


        if (destroyWhenActivated)
        {
            //will destroy the shoutbox so it cannot be repeated.
            Destroy(gameObject);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        //if require button press is true            
    //        if(requireButtonPress)
    //        {
    //            //enable the worldCanvas
    //            //Debug.Log("Press E to interact");
    //            EnableWorldCanvas();
    //            waitForPress = true;
    //            return;               
    //        }
    //        else
    //        {
    //            //Run SetText.
    //            SetText();                
    //        }
    //    }
    //}

    //void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        waitForPress = false;
    //    }
    //}

    public void EnableWorldCanvas()
    {
        //Activates the worldCanvas and its text
        worldCanvas.SetActive(true);
        //interactText.SetActive(true);
    }

    public void DisableWorldCanvas()
    {
        //Deactivates the worldCanvas and its text
        worldCanvas.SetActive(false);
        //interactText.SetActive(false);
    }
}
