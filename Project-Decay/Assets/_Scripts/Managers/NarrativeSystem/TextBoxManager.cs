using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class TextBoxManager : MonoBehaviour {

    FadeManager fader;
    public GameObject textBox;
    public Text theText;
    //Public text variable where text file will be stored
    TextAsset textFile;
    string[] textLines;

    public int currentLine;
    public int endAtLine;

    public PlayerController player;
    Animator playerAnim;

    public bool isActive;

    public bool stopPlayerMovement;

    //When text is scrolling across the screen.
    private bool isTyping = false;
    //Cancel text typing and make it insantly appear.
    private bool cancelTyping = false;
    //Determines speed of text typing.
    public float typeSpeed;

    // Use this for initialization
    void Start()
    {
        fader = FindObjectOfType<FadeManager>();

        //checking if their is a textFile
        if (textFile != null)
        {
            //Getting our text lines array
            //grabbing the text within the text file and split it wherever their is a return.
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextBox();
        }
        else
        {
            DisableTextBox();
        }

    }

    void Update()
    {
        if(!isActive)
        {
            return;
        }            
        //check after if any errors appear
           // theText.text = textLines[currentLine];
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!isTyping)
            {
                currentLine += 1;
                               
                if (currentLine > endAtLine)
                {
                    DisableTextBox();
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }

                if( currentLine > endAtLine)
                {
                    // SceneManager.LoadScene("01");
                    //Calls a function in the Fader script which runs a couroutine which then loads the new scene.
                    fader.SceneFadeOutBlack();
                }

            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        } 
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        //Checks the current letter in string
        int letter = 0;
        theText.text = "";
        //setting both bools to the correct values.
        isTyping = true;
        cancelTyping = false;
        //while isTyping is true and the player has not canceled typing run the while loop
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            //Go to the next letter
            theText.text += lineOfText[letter];
            letter += 1;
            //typing determined by the time speed
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        isActive = true;      

        StartCoroutine(TextScroll(textLines[currentLine]));
    }

    public void DisableTextBox()
    {
        textBox.SetActive(false);
        isActive = false;

        //player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));

        }
    }   
}
