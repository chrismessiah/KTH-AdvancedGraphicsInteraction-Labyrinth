using UnityEngine;
using System.Collections;
using Valve.VR;

public class VRMovement : MonoBehaviour
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
        // Doesn't need to update every frame, used for debugging and changing speed during running the game
        moveSpeed = moveManager.moveSpeed;

        
        //If finger is on touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {

            // CAMERA DIRECTIONS
            //Vector3 forwardXZ = new Vector3(player.transform.forward
            //Vector3 rightXZ = new Vector3(playerCamera.transform.right.x, 0, playerCamera.transform.right.z);

            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            //// TEST

            Vector3 localForward = player.transform.InverseTransformDirection(transform.forward);
            Vector3 localRight = player.transform.InverseTransformDirection(transform.right);
            //Vector3 localForward = transform.rotation * transform.forward;
            //Vector3 localRight = transform.rotation * transform.right;
            //localForward = transform.InverseTransformDirection(transform.forward);
            //localRight = transform.InverseTransformDirection(transform.right);

            Vector3 bla = new Vector3(localForward.normalized.x * player.transform.forward.x, player.transform.forward.y, localForward.normalized.z * player.transform.forward.z);
            Vector3 blar = new Vector3(localRight.normalized.x * player.transform.right.x, player.transform.right.y, localRight.normalized.z * player.transform.right.z);
            //Vector3 movement = player.transform.forward.normalized * localForward.z * moveSpeed * touchpad.y + player.transform.right.normalized * localRight.x * moveSpeed * touchpad.x;
            Vector3 movement = bla * moveSpeed * touchpad.y + blar * moveSpeed * touchpad.x;
            /////





            // CAMERA MOVE (HEAD DIRECTION MOVEMENT)
            // Vector3 movement = playerCamera.transform.forward.normalized * moveSpeed * touchpad.y + playerCamera.transform.right.normalized * moveSpeed * touchpad.x;

            // CONTROLLER MOVE (TORSO DIRECTION MOVEMENT)
            //Vector3 movement = (transform.forward.normalized * moveSpeed * touchpad.y + transform.right.normalized * moveSpeed * touchpad.x);
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