using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour {

    //Public text variable where text file will be stored
    public TextAsset textFile;
    public string[] textLines;

	// Use this for initialization
	void Start () {
        //checking if their is a textFile
        if(textFile != null)
        {
            //Getting our text lines array
            //grabbing the text within the text file and split it wherever their is a return.
            textLines = (textFile.text.Split('\n'));
        }
	
	}
	
	
}
