using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour {

    #region Movement
    // Variables and declarations
    public float moveForce = 0f;
    private Rigidbody rb;
    public bool inventoryOpen = false;
    public bool mouseEnabled = false;
    
    public int jumpForce = 200;
    public bool canJump;

    [NonSerialized]
    public bool movementEnable = true;
    #endregion

    private WeaponReloader weaponReloader;    
    
    #region Camera Control

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensitivity;
    }
    CameraLook cameraLook;
    Vector2 mouseInput;
    Vector2 playerMouseInput;
    [SerializeField] MouseInput MouseControl;

    private Crosshair myCrosshair;
    private Crosshair Crosshair
    {
        get
        {
            if (myCrosshair == null)
                myCrosshair = GetComponentInChildren<Crosshair>();
            return myCrosshair;
        }
    }
    #endregion

    bool isAlive;

    // Use this for initialization
    void Start()
    {      
        cameraLook = FindObjectOfType<CameraLook>();
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        weaponReloader = FindObjectOfType<WeaponReloader>();
    }
	
	// Update is called once per frame
	void Update ()
    {        
        Move();
        //Jump();
        ReloadPressed();
        enableMouse();

        playerMouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        //Mouse controls.
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerMouseInput.x, 1f / MouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerMouseInput.y, 1f / MouseControl.Damping.y);
        transform.Rotate(Vector3.up * mouseInput.x * MouseControl.Sensitivity.x);

        //Crosshair.LookHeight(mouseInput.y * MouseControl.Sensitivity.y);

    }

    public void Move()
    {
        if (movementEnable)
        {
            // Gets the horizontal input
            float horizontal = Input.GetAxisRaw("Horizontal") * moveForce / 2;

            // If the movement is true ( more than 0) then animate the player


            // Gets the vertical Input
            float vertical = Input.GetAxisRaw("Vertical") * moveForce;
            //  If the movement is true ( more than 0) then animate the player

            rb.velocity = transform.TransformVector(horizontal, 0, vertical);
        }
    }

    //public void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        print("Space Pressed");
    //        rb.AddForce(0, jumpForce * 10, 0, ForceMode.Impulse);
    //    }
    //}

    public void ReloadPressed()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reload Pressed");
            weaponReloader.ReloadCheck();
        }
    }

    public void enableMouse()
    {
        if (Input.GetKey(KeyCode.F))
        {
            mouseEnabled = true;
            if (mouseEnabled)
            {
                Cursor.visible = true;
            }
            mouseEnabled = false;
        }
        else if (mouseEnabled == false)
        {
            Cursor.visible = false;
        }

    }

    // Enables and disables movement
    public void enableMovement()
    {
        cameraLook.movementEnabled = !cameraLook.movementEnabled;
    }

    // When item is picked up/ used - displays message on screen
    
    public void Death()
    {
        enableMovement();
        Debug.Log("Player is dead");
    }

    
    // Disables movement for the player
    public void DisableMovement()
    {
        StartCoroutine(disableMovement());
    }

    IEnumerator disableMovement()
    {        
        movementEnable = false;
        yield return new WaitForSeconds(1);
        movementEnable = true;
    }

}
