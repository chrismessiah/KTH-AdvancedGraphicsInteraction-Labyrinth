using System.Collections;
using System.Collections.Generic;
using UnityEngine.PostProcessing;
using UnityEngine;

public class MovementManager : MonoBehaviour {

    public float moveSpeed;
    public bool controllerMove;
    public GameObject player
    {
        get { return gameObject; }
    }

    public PostProcessingProfile pp;

    public Rigidbody rb {
        get { return GetComponent<Rigidbody>(); }
    }
    private float t = 0;


    void Update(){
		if(Input.GetKeyDown("x")){
			if(controllerMove == true){
				controllerMove = false;
			} else {
				controllerMove = true;
			}
		}

        if (rb.velocity.magnitude > 1)
        {
            if (t < 1)
            {
                t += 0.01f;
            }
        }
        else
        {
            if (t > 0)
            {
                t -= 0.03f;
            }
        }
        VignetteModel.Settings vignettetmp = pp.vignette.settings;
        vignettetmp.intensity = Mathf.Lerp(0.05f, 0.5f, t);
        pp.vignette.settings = vignettetmp;
    }

}
