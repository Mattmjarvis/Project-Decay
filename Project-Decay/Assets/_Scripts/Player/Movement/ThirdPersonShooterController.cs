

using UnityEngine;
using System.Collections;

public class ThirdPersonShooterController : MonoBehaviour {

    //This variable indicates how is the current state of character.
    private int state;

    //This variable indicates if the player is aiming or not.
    private bool looking; 

    //Define the turning speed.
    private float turningSpeed = 4.0f;

    //Defines move speed
    public float moveSpeed;    

    private float horizontal;

   //private Animator animator;
    private Vector3 screenCentre;
    private CursorLockMode mouseLocked;
    

    public bool blockControl;

    private WeaponReloader weaponReloader;


    //Get the camera properties.
    public Camera camera; 

    void Start ()
    {
        screenCentre.x = 0.5f;
        screenCentre.y = 0.5f;
        screenCentre.z = 0f;
        //animator = GetComponentInChildren<Animator>();
        state = 0;
        looking = false;
        blockControl = false;
        horizontal = transform.eulerAngles.y;

        weaponReloader = FindObjectOfType<WeaponReloader>();
    }

    void Update ()
    {
        FocusRaycast();
        if (blockControl == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Control();
            MovePlayer();
            //AnimatePlayer();
            FocusCamera();
            ReloadPressed();
        }
    }

    //private void AnimatePlayer()
    //{
    //    animator.SetInteger("State", state);
    //}

    private void Control()
    {
        /*
        State:
        01 = Walking
        02 = Running
        03 = Walking Back
        04 = Walking Right
        05 = Walking Left
        */

        if (Input.GetKeyDown("w"))
        {
            state = 1;
        }
        if (Input.GetKeyUp("w") && state == 1)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("d")) { state = 4; }
        }
        if (Input.GetKeyUp("w") && state == 2)
        {
            state = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && state == 1)
        {
            state = 2;
            if (looking == true)
            {
                looking = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && state == 2) { state = 1; }
                
        if (Input.GetKeyDown("s"))
        {
            state = 3;
        }
        if (Input.GetKeyUp("s") && state == 3)
        {
            state = 0;
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("d")) { state = 4; }
            if (Input.GetKey("w")) { state = 1; }
        }

        if (Input.GetKeyDown("d"))
        {
            state = 4;
        }
        if (Input.GetKeyUp("d") && state == 4)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("a")) { state = 5; }
            if (Input.GetKey("w")) { state = 1; }

        }

        if (Input.GetKeyDown("a"))
        {
            state = 5;
        }
        if (Input.GetKeyUp("a") && state == 5)
        {
            state = 0;
            if (Input.GetKey("s")) { state = 3; }
            if (Input.GetKey("d")) { state = 4; }
            if (Input.GetKey("w")) { state = 1; }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            looking = true;
            if (state == 2)
            {
                state = 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            looking = false;
        }
    }

    private void FocusCamera()
    {
        if (looking == true && camera.fieldOfView > 50)
        {
            camera.fieldOfView = camera.fieldOfView - 65f * Time.deltaTime;
        }
        if (looking == false && camera.fieldOfView < 60)
        {
            camera.fieldOfView = camera.fieldOfView + 65.0f * Time.deltaTime;
        }
    }

    private void FocusRaycast()
    {
        //RaycastHit hitInfo;
        //Ray cameraRay = camera.ViewportPointToRay(screenCentre);
    }
    
    private void MovePlayer()
    {
        var mouseHorizontal = Input.GetAxis("Mouse X");
        horizontal = (horizontal + turningSpeed * mouseHorizontal) % 360f;
        transform.rotation = Quaternion.AngleAxis(horizontal, Vector3.up);

        if (state == 0) { transform.Translate(0, 0, 0); }
        if (state == 1) { transform.Translate(0, 0, 3.0f * Time.deltaTime * moveSpeed); }
        if (state == 2) { transform.Translate(0, 0, 5.0f * Time.deltaTime * moveSpeed); }
        if (state == 3) { transform.Translate(0, 0, -3f * Time.deltaTime * moveSpeed); }
        if (state == 4) { transform.Translate(8f * Time.deltaTime, 0, 0 * moveSpeed); }
        if (state == 5) { transform.Translate(-8f * Time.deltaTime, 0, 0 * moveSpeed); }
    }

    public bool ReturnLooking()
    {
        return looking;
    }
   
    public void ReloadPressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Debug.Log("Reload Pressed");
            weaponReloader.ReloadCheck();
        }
    }
}
