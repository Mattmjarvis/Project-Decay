using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    Coroutine textRoutine = null;
    CutsceneController cutscene;
    public Text textBox;
    float type_speed = 0.02f;
    
    public string[] textLog;
    public int nextText = -1;

    public AudioSource source;

    // Use this for initialization
    void Start()
    {
        cutscene = FindObjectOfType<CutsceneController>();
        StartTyping();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Starts the typing
    public void StartTyping()
    {
        textBox.text = null;
        StartCoroutine(TypeWriter());
    }

    // Stops the typing
    public void StopTyping()
    {
        source.Stop();
        StopAllCoroutines();
        textBox.text = null;
    }

    // Start writing when scene loads
    IEnumerator TypeWriter()
    {

        nextText++; // Increments Text
        int charNumber = 0;

        //Small delay until start
        yield return new WaitForSeconds(3.0f);

        if (textLog[nextText] == "")
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }


        //loops through each letter with a small delay in between each
        foreach (char c in textLog[nextText])
        {
            textBox.text += c;
            charNumber++;
            yield return new WaitForSeconds(type_speed);

            //Debug.Log(charNumber);   
            
            if(charNumber == textLog[nextText].Length)
            {
                source.Stop();
            }


        }



    }
}