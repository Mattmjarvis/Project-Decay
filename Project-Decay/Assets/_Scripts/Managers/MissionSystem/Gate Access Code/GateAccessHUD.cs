using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GateAccessHUD : MonoBehaviour {

    public Image codeHUD;
    public bool enableHUD = false;

	// Use this for initialization
	void Awake () {

        codeHUD.fillAmount = 0f;
        foreach (Text text in codeHUD.GetComponentsInChildren<Text>())
        {
            text.gameObject.SetActive(false);
        }
    }

    // Shows or hides the gate access code
    public void ShowHideCodeHUD()
    {
        StartCoroutine(EnableDisableCodeHUD());
    }

    IEnumerator EnableDisableCodeHUD()
    {
        float time = codeHUD.fillAmount;

        // Hides the HUD Mission Image
        if (enableHUD == true)
        {
            // Disables all text components in the HUD mission
            foreach (Text text in codeHUD.GetComponentsInChildren<Text>())
            {
                text.gameObject.SetActive(false);
            }

            // Reduces fill amount of HUD image each update
            while (time > 0f)
            {
                codeHUD.fillAmount -= 0.1f;
                time = codeHUD.fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        else if (enableHUD == false)
        {
            while (time < 1f)
            {
                codeHUD.fillAmount += 0.1f;
                time = codeHUD.fillAmount;
                yield return new WaitForFixedUpdate();
            }
        }

        if (time == 1f)
        {
            enableHUD = true;
            foreach (Text text in codeHUD.GetComponentsInChildren<Text>(true))
            {
                text.gameObject.SetActive(true);
            }
            StopCoroutine(EnableDisableCodeHUD());
        }

        else if (time == 0f)
        {
            enableHUD = false;
            StopCoroutine(EnableDisableCodeHUD());

        }

    }
}
