using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    // These components will show the supply crate on the compass
    public GameObject supplyCrateImage;
    public GameObject supplyCrate;
    private Vector3 cratePosition;
    private Transform playerTransform;
    private Vector3 playerPosition;
    private PlayerHealth playerHealth;

    public  bool compassEnabled;

    // Use this for initialization
    void Start()
    {        
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Moves the image in relation to the camera
    public void Move()
    {
        if(playerHealth.health <= 0)
        {
            return;
        }
            playerPosition = playerTransform.position;
            // Gets the camera rotation (x) to move the local position of the image
            float xF = 180f - (playerTransform.eulerAngles.y * 360.0f / 360.0f);

            // Moves the image based on the camera rotation
            transform.localPosition = new Vector3(xF, 0, 0);
    }


    // Enables the crate icon to appear on the compass
    public void ShowCrateOnCompass()
    {
        // Finds the crate
        supplyCrate = GameObject.FindGameObjectWithTag("SupplyCrate");
        if(supplyCrate == null)
        {
            Debug.Log("Cant find crate");
            return;
        }

        // Enables the compass sprite
        supplyCrateImage.SetActive(true);
        StartCoroutine(UpdateCompass());

    }

    IEnumerator UpdateCompass()
    {
        if(supplyCrate == null)
        {
            StopCoroutine(UpdateCompass());
            Debug.Log("Supply crate null");
        }
        while (true)
        {
            // Gets the crate position and applies it to compass
            cratePosition = supplyCrate.transform.position;

            Vector3 targetDir = cratePosition - playerTransform.position;
            double dist = Vector3.Distance(cratePosition, playerPosition);
            double z_dist = (double)targetDir.z;// playerPosition.z - cratePosition.z;
            double angleR = Mathf.Asin((float)(z_dist / dist));
            double angleD = Mathf.PI / 2 - angleR;

            // Radian to degree
            double angleDegrees = playerTransform.eulerAngles.y  + angleD * Mathf.Rad2Deg;
            if ( targetDir.x >= 0f)
            {
                angleDegrees = playerTransform.eulerAngles.y - angleD * Mathf.Rad2Deg;
            }


            // Gets the camera rotation (x) to move the local position of the image
            if( !double.IsNaN(angleDegrees))
            {
                float xF = 360f - ((float)angleDegrees * 360.0f / 360.0f);
                if (targetDir.z < 0f && targetDir.x > 0f && playerTransform.eulerAngles.y < 270f)
                {
                    xF = - ((float)angleDegrees * 360.0f / 360.0f);
                   // Debug.Log("A angleDegrees: " + angleDegrees + " xf:" + xF + " playerTransform.eulerAngles.y:" + playerTransform.eulerAngles.y + " " + targetDir);
                }
                else if (playerTransform.eulerAngles.y < 180f && targetDir.x >= 0f)
                {
                    // && targetDir.x > 0
                    xF = -((float)angleDegrees * 360.0f / 360.0f);
                   // Debug.Log("B angleDegrees: " + angleDegrees + " xf:" + xF + " playerTransform.eulerAngles.y:" + playerTransform.eulerAngles.y + " " + targetDir);
                }else if(playerTransform.eulerAngles.y < 90f)
                {
                    xF = -((float)angleDegrees * 360.0f / 360.0f);
                   // Debug.Log("C angleDegrees: " + angleDegrees + " xf:" + xF + " playerTransform.eulerAngles.y:" + playerTransform.eulerAngles.y + " " + targetDir);
                }

              
               

                // Moves the image based on the camera rotation
                supplyCrateImage.transform.localPosition = new Vector3(xF, supplyCrateImage.transform.localPosition.y, 0);
            }
        
            // Interval between updating the icon
            yield return new WaitForSeconds(0.01f);
        }

    }
}
