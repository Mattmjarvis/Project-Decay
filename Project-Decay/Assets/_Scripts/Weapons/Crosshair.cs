using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{    
    void Update()
    {       
        var mousePos = Input.mousePosition;
        //Clamping the mouse using the percentages of the screens width and height.
        mousePos.x = Mathf.Clamp(mousePos.x, Screen.width * 0.3f, Screen.width - (Screen.width * 0.3f));
        mousePos.y = Mathf.Clamp(mousePos.y, Screen.height * 0.3f, Screen.height - (Screen.height * 0.3f));       

        transform.position = mousePos;
    }
}
