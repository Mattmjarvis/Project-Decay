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

    // Use this for initialization
    void Start()
    {
        
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        Debug.Log("playerTransform ys: " + playerTransform.eulerAngles.y);
        ShowCrateOnCompass();
    }

    // Moves the image in relation to the camera
    public void Move()
    {
    
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
        supplyCrate = GameObject.Find("SupplyCrate");
       


        // Enables the compass sprite
        supplyCrateImage.SetActive(true);
        StartCoroutine(UpdateCompass());

    }

    IEnumerator UpdateCompass()
    {
        while (true)
        {
            // Gets the crate position and applies it to compass
            cratePosition = supplyCrate.transform.position;
            Debug.Log("player : " + playerPosition);
            Debug.Log("cratePosition : " + cratePosition);
            Vector3 targetDir = cratePosition - playerTransform.position;
            float y = Vector3.Angle(targetDir, playerTransform.forward);
            float dist = Vector3.Distance(cratePosition, playerPosition);
            float z_dist =  playerPosition.z - cratePosition.z;
            float x_dist = playerPosition.x - cratePosition.x;
            float angleR = Mathf.Sin(z_dist / dist);
            float angleD = angleR * 180f / Mathf.PI;
          
        
            // Gets the camera rotation (x) to move the local position of the image
            float xF = 180f - ( (y - playerTransform.eulerAngles.y ) * 360.0f / 360.0f);
           // float xF =   (playerTransform.eulerAngles.y - angleD * 360.0f / 360.0f) ;


            Debug.Log("y:  " + y +  " angleD: " + angleD + " ply: " + playerTransform.eulerAngles.y + " xf:" + xF);

            // Moves the image based on the camera rotation
            supplyCrateImage.transform.localPosition = new Vector3(xF, supplyCrateImage.transform.localPosition.y, 0);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
