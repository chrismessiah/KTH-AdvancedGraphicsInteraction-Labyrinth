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
    private bool controllerMove = true;
    private Transform moveTransform;
    private Vector3 localForward;
    private Vector3 localRight;
    private Vector3 worldForward;
    private Vector3 worldRight;
    private Vector3 movementVector;

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
        controllerMove = moveManager.controllerMove;
    }

    // Update is called once per frame
    void Update()
    {
        // Doesn't need to update every frame, used for debugging and changing speed during running the game
        // Remove from final
        moveSpeed = moveManager.moveSpeed;
        controllerMove = moveManager.controllerMove;

        // Also used for testing different locomotion types
        // controllerMove: MOVE BY CONTROLLER ("TORSO" DIRECTION)
        // camera move: MOVE BY CAMERA ("HEAD" DIRECTION)
        if (controllerMove)
        {
            moveTransform = transform;
        } else
        {
            moveTransform = playerCamera.transform;
        }
        //
        
        //If finger is on touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //Read the touchpad values
            touchpad = device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);

            // MOVE
            localForward = player.transform.InverseTransformDirection(moveTransform.forward.normalized);
            localRight = player.transform.InverseTransformDirection(moveTransform.right.normalized);
            worldForward = player.transform.TransformDirection(new Vector3(localForward.x, 0, localForward.z));
            worldRight = player.transform.TransformDirection(new Vector3(localRight.x, 0, localRight.z));
            movementVector = worldForward * moveSpeed * touchpad.y + worldRight * moveSpeed * touchpad.x;       

            rb.AddForce(movementVector, ForceMode.Acceleration);
            

        }
    }
}