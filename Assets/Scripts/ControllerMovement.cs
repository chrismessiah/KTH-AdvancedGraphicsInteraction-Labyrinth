using UnityEngine;
using System.Collections;
using Valve.VR;

public class ControllerMovement : MonoBehaviour
{
    //public GameObject world;
    public GameObject playerCamera;
    private float moveSpeed;
    private GameObject player;
    private Rigidbody rb;

    SteamVR_Controller.Device device 
    {
        get { return SteamVR_Controller.Input((int)controller.index); }
    }

    private MovementManager moveManager
    {
        get { return transform.parent.parent.GetComponent<MovementManager>(); }
    }

    SteamVR_TrackedObject controller;
    Vector2 touchpad;




    void Awake(){
        controller = gameObject.GetComponent<SteamVR_TrackedObject>();
        
    }

    void Start()
    {
        player = transform.parent.parent.gameObject;
        rb = player.GetComponent<Rigidbody>();
        moveSpeed = moveManager.moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = moveManager.moveSpeed;
        //device = SteamVR_Controller.Input((int)controller.index);
        //If finger is on touchpad
        
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {

            // CAMERA DIRECTIONS
            //Vector3 forwardXZ = new Vector3(playerCamera.transform.forward.x, 0, playerCamera.transform.forward.z);
            //Vector3 rightXZ = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);

            // CONTROLLER DIRECTIONS
            //Vector3 forwardXZ = new Vector3(player.transform.forward.x, 0, player.transform.forward.z);
            //Vector3 rightXZ = new Vector3(player.transform.right.x, 0, player.transform.right.z);

            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
            Vector3 movement = playerCamera.transform.forward.normalized * moveSpeed * touchpad.y + playerCamera.transform.right.normalized * moveSpeed * touchpad.x;
            rb.AddForce(movement, ForceMode.Acceleration);

            // MOVE
            //rb.AddForce(forwardXZ * touchpad.y * moveManager.moveSpeed, ForceMode.Acceleration);
            //rb.AddForce(rightXZ * touchpad.x * moveManager.moveSpeed, ForceMode.Acceleration);



            //player.transform.position += transform.right * 0.1f * touchpad.x;
            //world.transform.Rotate(0, 0, -1*touchpad.y*moveSpeed);
            //player.transform.position = new Vector3(player.transform.position.x + (-touchpad.y / 100f), player.transform.position.y, player.transform.position.z + (touchpad.x / 100f));
            //world.transform.Translate(0,0, 0.1f * touchpad.x * moveSpeed);



        }
    }
}